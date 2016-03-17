using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Robit {
    public static class Globals {
        public static readonly Cpu.Pin Pin_LeftMotorSpeed = Pins.GPIO_PIN_D6;
        public static readonly Cpu.Pin Pin_LeftMotorDirection = Pins.GPIO_PIN_D7;
        public static readonly Cpu.Pin Pin_RightMotorSpeed = Pins.GPIO_PIN_D5;
        public static readonly Cpu.Pin Pin_RightMotorDirection = Pins.GPIO_PIN_D4;
        public static bool Moving { get; set; }

        public static int LeftDriveSpeed { get; set; }
        public static int RightDriveSpeed { get; set; }

        public static bool CancelMovementAlarmTriggered = false;
        public static bool MovementAlarm = false;
    }
}