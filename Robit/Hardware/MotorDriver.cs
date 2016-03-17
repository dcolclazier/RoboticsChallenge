using System;
using Toolbox.NETMF.Hardware;

namespace Robit {
    
    public class MotorDriver {
        
        public enum Direction {
            forward, reverse
        }
        private readonly HBridge _HBridge;

        public MotorDriver() {
            _HBridge = new HBridge(new Netduino.PWM(Globals.Pin_LeftMotorSpeed), Globals.Pin_LeftMotorDirection,
                new Netduino.PWM(Globals.Pin_RightMotorSpeed), Globals.Pin_RightMotorDirection);

            Brain.OnAlarmReceived += HandleMovementAlarm;
        }

        private void HandleMovementAlarm(AlarmTriggers alarm) {

            if (alarm != AlarmTriggers.CancelRobotMovement) return;
            
            //trigger the flag that will cancel movement
            Globals.CancelMovementAlarmTriggered = true;
            
        }

        public void Drive(Direction direction, sbyte speed, int time) {
            switch (direction) {
                case Direction.forward:
                    _HBridge.SetState(HBridge.Motors.Motor1, speed); //half speed forward
                    _HBridge.SetState(HBridge.Motors.Motor2, speed); //half speed reverse
                    break;
                case Direction.reverse:
                    _HBridge.SetState(HBridge.Motors.Motor1, (sbyte)-speed); //half speed forward
                    _HBridge.SetState(HBridge.Motors.Motor2, (sbyte)-speed); //half speed reverse
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
            Globals.Moving = true;
            Globals.LeftDriveSpeed = speed;
            Globals.RightDriveSpeed = speed;

            if (!Brain.Instance.Sleep(time, ref Globals.CancelMovementAlarmTriggered)) {
                //reset the alarm
                Globals.CancelMovementAlarmTriggered = false;
            }

            Stop();
        }

        public void Stop() {
            _HBridge.SetState(HBridge.Motors.Motor1, 0); 
            _HBridge.SetState(HBridge.Motors.Motor2, 0);
            Globals.Moving = false;
            Globals.LeftDriveSpeed = 0;
            Globals.RightDriveSpeed = 0;
        }

        
    }

    
}