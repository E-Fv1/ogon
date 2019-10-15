using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Text;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace Hero_Simple_Application4
{
    public class Program
    {
        static AnalogInput analogInput0 = new AnalogInput(CTRE.HERO.IO.Port1.Analog_Pin3);
        static AnalogInput analogInput1 = new AnalogInput(CTRE.HERO.IO.Port1.Analog_Pin4);
        static AnalogInput analogInput2 = new AnalogInput(CTRE.HERO.IO.Port1.Analog_Pin5);

        /*
        static TalonSRX rightSlave = new TalonSRX(4);
        static TalonSRX right = new TalonSRX(3);
        static TalonSRX leftSlave = new TalonSRX(2);
        static TalonSRX left = new TalonSRX(1);
        */

        static StringBuilder stringBuilder = new StringBuilder();

        /* create a gamepad object */
        CTRE.Phoenix.Controller.GameController myGamepad = new
        CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(0));

        public static void Main()
        {
            /*
            double read0;
            double read1;
            double read2;
            */

            /* create a gamepad object */
            CTRE.Phoenix.Controller.GameController myGamepad = new CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(0));

            /* simple counter to print and watch using the debugger */
            int counter = 0;
            /* loop forever */
            while (true)
            {
                /*
                read0 = analogInput0.Read();
                read1 = analogInput1.Read();
                read2 = analogInput2.Read();
                */

                if (myGamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected)
                {
                    /* print the axis values */
                    Debug.Print("axis0 - LSH:" + myGamepad.GetAxis(0)); //Left Stick Horizontal
                    Debug.Print("axis1 - LSV:" + myGamepad.GetAxis(1)); //Left Stick Vertical
                    Debug.Print("axis2 - RSH:" + myGamepad.GetAxis(2)); //Right Stick Horizontal
                    Debug.Print("axis3 - RSV:" + myGamepad.GetAxis(5)); //Right Stick Vertical
                    /* allow motor control */+
                    CTRE.Phoenix.Watchdog.Feed();
                }


                /* Move Robot */
                Drive();
                /* print whatever is in our string builder */
                //Debug.Print(stringBuilder.ToString());
                //stringBuilder.Clear();
                /* feed watchdog to keep Talon's enabled */
                CTRE.Phoenix.Watchdog.Feed();
                /* run this task every 20ms */
                Thread.Sleep(20);
                /* print the three analog inputs as three columns */
                //Debug.Print("Counter Value: " + counter);
                //Debug.Print("" + read0 + "\t" + read1 + "\t" + read2);


                /* increment counter */
                ++counter; /* try to land a breakpoint here and hover over 'counter' to see it's current value.  Or add it to the Watch Tab */

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
            }
        }
        static void Deadband(ref float value)
        {
            if (value < -0.10)
            {
                /* outside of deadband */
            }
            else if (value > +0.10)
            {
                /* outside of deadband */
            }
            else
            {
                /* within 10% so zero it */
                value = 0;
            }
        }
        static void Drive()
        {
           /* (null == _gamepad)
                _gamepad = new GameController(UsbHostDevice.GetInstance());

            float x = _gamepad.GetAxis(0);
            float y = -1 * _gamepad.GetAxis(1);
            float twist = _gamepad.GetAxis(2);

            Deadband(ref x);
            Deadband(ref y);
            Deadband(ref twist);

            float leftThrot = y + twist;
            float rightThrot = y - twist;

            left.Set(ControlMode.PercentOutput, leftThrot);
            leftSlave.Set(ControlMode.PercentOutput, leftThrot);
            right.Set(ControlMode.PercentOutput, -rightThrot);
            rightSlave.Set(ControlMode.PercentOutput, -rightThrot);

            stringBuilder.Append("\t");
            stringBuilder.Append(x);
            stringBuilder.Append("\t");
            stringBuilder.Append(y);
            stringBuilder.Append("\t");
            stringBuilder.Append(twist);
            */
        }
    }
}
