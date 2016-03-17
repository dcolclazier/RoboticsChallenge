using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
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
    public class Program
    {
        public static void Main() {

            var action = new Action(test);
            Brain.Instance.Execute(action);

        }
        //needs to check for movement cancel alarm...
        private static void test() {

            Brain.Instance.DriveTrain.Drive(MotorDriver.Direction.forward, 50, 5000);
            Brain.Instance.DriveTrain.Stop();

            Brain.Instance.Sleep(1000, ref Globals.RestAlarm);

            Brain.Instance.DriveTrain.Drive(MotorDriver.Direction.forward, 100, 1000);
            Brain.Instance.DriveTrain.Stop();



        }
    }
}
