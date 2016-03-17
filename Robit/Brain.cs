using System;
using System.Threading;

namespace Robit {
   
    public class Brain {
        //singleton - only one brain, accessable globaly
        private static Brain _instance;
        public static Brain Instance => _instance ?? (_instance = new Brain());

        //brain should have reference to all hardware
        public MotorDriver DriveTrain { get; private set; }



        private Brain() {
            DriveTrain = new MotorDriver();
        }

        public void Execute(Action action) {
            ThreadPool.QueueAction(action);
        }
        

        public bool Sleep(int i, ref bool alarmTriggered) {
            var counter = i;
            while (counter > 0) {
                counter -= 50;
                Thread.Sleep(50);
                if (alarmTriggered) return false ;
            }
            return true;
        }
        public bool Sleep(int i, ref bool alarmTriggered, ref bool altAlarm) {
            var counter = i;
            while (counter > 0) {
                counter -= 50;
                Thread.Sleep(50);
                if (alarmTriggered || altAlarm) return false ;
            }
            return true;
        }
        public bool Sleep(int i, ref bool alarmTriggered, ref bool altAlarm, ref bool altAlarm2) {
            var counter = i;
            while (counter > 0) {
                counter -= 50;
                Thread.Sleep(50);
                if (alarmTriggered || altAlarm || altAlarm2) return false ;
            }
            return true;
        }

        public delegate void EventTriggered(RobotEventType eventType, IEventData eventData);
        public static event EventTriggered OnEventTriggered;

        public delegate void AlarmReceived(AlarmTriggers alarm);
        public static event AlarmReceived OnAlarmReceived;

        public void TriggerEvent(RobotEventType eventType, IEventData eventData) { 
            OnEventTriggered?.Invoke(eventType, eventData);
        }
        public void TriggerAlarm(AlarmTriggers alarm) {
            OnAlarmReceived?.Invoke(alarm);
        }
    }
}




