using System.Threading;

namespace Robit {
    public class Action {
        public readonly ThreadStart InnerAction = null;

        public readonly RobotEventType EventType;
        public readonly IEventData EventData;

        public bool Persistent { get; private set; }

        public Action(ThreadStart innerAction, RobotEventType eventType = RobotEventType.None, IEventData eventData = null, bool persistent = false) {
            InnerAction = innerAction;
            EventType = eventType;
            EventData = eventData;
            Persistent = persistent;
        }

        public void Persistence(bool persistant) {
            Persistent = persistant;
        }
    }
}