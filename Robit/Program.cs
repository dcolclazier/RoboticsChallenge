using System.Threading;

namespace Robit
{
    public class Program
    {
        public static void Main() {

            //enqueue an action to move forward at half speed for 5 seconds... will run in seperate thread.
            //will stop if a CancelRobotMovement alarm is triggered by any thread... hopefully.
            var moveAction = new Action(() => {
                Brain.Instance.DriveTrain.Drive(MotorDriver.Direction.forward, 50, 5000);
            });
            Brain.Instance.Execute(moveAction);

            //wait on the main thread for 1 second... the move action should have been running for 1 second
            //by the time this finishes.
            Thread.Sleep(1000);

            //enqueue an action to cancel the robot movement, by triggering the appropriate alarm.
            var stopAction = new Action(() => {
                Brain.Instance.TriggerAlarm(AlarmTriggers.CancelRobotMovement);
            });
            Brain.Instance.Execute(stopAction);

            //end result - motors should run for 1 second, then get cancelled, stopping them...

        }
    }
}



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
