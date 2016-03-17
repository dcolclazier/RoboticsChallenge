using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Toolbox.NETMF.Hardware;
/*
Ok, so robot has senses
Robot will be receiving sensory input at unknown, random times
Robot will need to react to sensory input
Robot should be able to remember certain things...
Robot needs to know about herself - 
    - moving? 
    - turning?
    - stable?
    - inclined?
    - tilted?
    - slipping? accelerometer vs motor revolutions!
    - stuck? same!
Robot attributes could spawn sensory input
    - if tilted too much, react
Robot needs to know about her environment
Robot needs to know about she's interacting with the environment

    Put yourself in the robots shoes... You're blind, mostly. You have no memory, or a very limited one. You're deaf, mostly.
    You might be able to see things in front of you in IR, and you might be able to hear them with sonar, but you have to second-guess
    yourself because the data could be bad. You will make mistakes, and will have to recover from them. It's gonna be pretty impossible to "recognize" anything...

What senses?
    -space around her
        -obstacles should be assigned with a 'severity' in the case of "i can't go anywhere"
        -ir sensor should have ability to rotate for wide field of view, and know rotation angle for "front" look
    -ground in front of her
        - a hole in the ground could be an obstacle...
    -her speed
    -her tilt
    -her speed relative to wheel speed (slipping?)

    -- layers...

        -- hardware

*/
namespace Robit
{
    public static class Globals {
        public static readonly Cpu.Pin Pin_LeftMotorSpeed = Pins.GPIO_PIN_D6;
        public static readonly Cpu.Pin Pin_LeftMotorDirection = Pins.GPIO_PIN_D7;
        public static readonly Cpu.Pin Pin_RightMotorSpeed = Pins.GPIO_PIN_D5;
        public static readonly Cpu.Pin Pin_RightMotorDirection = Pins.GPIO_PIN_D4;
        public static bool Moving { get; set; }
        public static bool Turning { get; set; }
        public static bool Scanning { get; set; }
        public static int LeftDriveSpeed { get; set; }
        public static int RightDriveSpeed { get; set; }
    }
    public class Program
    {
        public static void Main() {

            var action = new Action(test);
            Brain.Instance.Execute(action);

        }
        //needs to check for movement cancel alarm...
        private static void test() {
            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor1, 50); //half speed forward
            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor2, -50); //half speed backward

            Thread.Sleep(5000); //run for 5 seconds /bug - should check for movement cancel alarm...

            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor1, -100); //full speed backward
            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor2, 100); //full speed forward

            Thread.Sleep(5000); //run for 5 seconds /bug - should check for movement cancel alarm...

            //stop both motors
            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor1, 0);
            Brain.Instance.MotorDriver.SetState(HBridge.Motors.Motor2, 0);
        }
    }
}
