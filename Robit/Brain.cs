using Toolbox.NETMF.Hardware;

namespace Robit {
    public class Brain {

        //singleton - only one brain, accessable globaly
        private static Brain _instance;
        public static Brain Instance => _instance ?? (_instance = new Brain());

        public HBridge MotorDriver { get; private set; }

        //brain should know about robots position, tilt, acceleration, and wheel speed 
        //brain should be able to set cancel alarms for event actions to check... or maybe directly cancel? eek.
        //      these stats could be globals that are updated by hardware sensor actions
        //we need an alarm, cancel system.... i.e. ground clearance gone, stop movement actions...


        //need to be able to cancel actions.............
        public Brain() {

            MotorDriver = new HBridge(new Netduino.PWM(Globals.Pin_LeftMotorSpeed), Globals.Pin_LeftMotorDirection,
                new Netduino.PWM(Globals.Pin_RightMotorSpeed), Globals.Pin_RightMotorDirection);
        }

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




