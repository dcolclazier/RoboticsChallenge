using System;
using Toolbox.NETMF.Hardware;

namespace Robit {
    public class MotorDriver {

        public enum Direction {
            forward, backward
        }
        private readonly HBridge _HBridge;

        public MotorDriver() {
            _HBridge = new HBridge(new Netduino.PWM(Globals.Pin_LeftMotorSpeed), Globals.Pin_LeftMotorDirection,
                new Netduino.PWM(Globals.Pin_RightMotorSpeed), Globals.Pin_RightMotorDirection);
        }

        public void Drive(Direction direction, sbyte speed, int time) {

            switch (direction) {
                case Direction.forward:
                    _HBridge.SetState(HBridge.Motors.Motor1, speed); //half speed forward
                    _HBridge.SetState(HBridge.Motors.Motor2, speed); //half speed backward
                    break;
                case Direction.backward:
                    _HBridge.SetState(HBridge.Motors.Motor1, (sbyte)-speed); //half speed forward
                    _HBridge.SetState(HBridge.Motors.Motor2, (sbyte)-speed); //half speed backward
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }
            Globals.Moving = true;
            Globals.Turning = false;
            Globals.LeftDriveSpeed = speed;
            Globals.RightDriveSpeed = speed;
            if (!Brain.Instance.Sleep(time, ref Globals.MovementAlarm)) {
                Stop();
            }
        }

        public void Stop() {
            _HBridge.SetState(HBridge.Motors.Motor1, 0); 
            _HBridge.SetState(HBridge.Motors.Motor2, 0);
            Globals.Moving = false;
            Globals.Turning = false;
            Globals.LeftDriveSpeed = 0;
            Globals.RightDriveSpeed = 0;
        }
    }
}