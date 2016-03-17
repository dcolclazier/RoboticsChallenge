using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;

namespace Robit {
    public class ThreadPool {
        
        
        //list of threads
        private static readonly ArrayList AvailableThreads = new ArrayList();
        
        //queue of Actions
        private static readonly Queue PendingActions = new Queue();
        
        //maximum thread count
        private const int MAX_THREADS = 3;

        public static void QueueAction(Action queueableAction) {
            lock (locker) {
                PendingActions.Enqueue(queueableAction);
            }

            //if we don't have all our threads, spin one up with this task.
            if (AvailableThreads.Count < MAX_THREADS) {
                var thread = new Thread(ExecuteAction);
                AvailableThreads.Add(thread);
                thread.Start();
            }
            //sets the threadsync, causing the actionExecute to stop waiting
            lock (locker) {
                ThreadSync.Set();
            }
        }

        private static void ExecuteAction() {
            while (true) {

                //wait for an action to be queued for execution.
                ThreadSync.WaitOne();

                Action nextActionToExecute = null;

                lock (locker) {
                    if (PendingActions.Count > 0) nextActionToExecute = PendingActions.Dequeue() as Action;
                    else ThreadSync.Reset();//tell other threads to wait, no actions to execute
                }

                if (nextActionToExecute == null) continue; //threadsync was reset, go back to waiting.

                //if we got here, we have a valid action to execute. do so.
                try {
                    //check for alarms...
                    nextActionToExecute.InnerAction(); //execute the action.
                    if (nextActionToExecute.EventType != RobotEventType.None) {
                        Brain.Instance.TriggerEvent(nextActionToExecute.EventType, nextActionToExecute.EventData);
                    }
                    if (nextActionToExecute.Persistent) QueueAction(nextActionToExecute);
                }
                catch (Exception e) {
                    Debug.Print("ThreadPool: Unhandled error executing action - " + e.Message + e.InnerException);
                    Debug.Print("StackTrace: " + e.StackTrace);
                }

            }
        }


        private static readonly ManualResetEvent ThreadSync = new ManualResetEvent(false);
        private static readonly object locker = new object();
    }
}