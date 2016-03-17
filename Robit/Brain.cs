namespace Robit {
    public class Brain {

        //singleton - only one brain, accessable globaly
        private static Brain _instance;
        public static Brain Instance => _instance ?? (_instance = new Brain());

        public void Execute(Action action) {
            ThreadPool.QueueAction(action);
        }

        public void TriggerEvent(RobotEventType eventType, IEventData eventData) {
            OnEventTriggered?.Invoke(eventType, eventData);
        }

        public delegate void EventTriggered(RobotEventType eventType, IEventData eventData);

        public static event EventTriggered OnEventTriggered;


    }
}




