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
       
        static StringBuilder stringBuilder = new StringBuilder();

        /* create a gamepad object */
        CTRE.Phoenix.Controller.GameController myGamepad = new
        CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(0));

        public static void Main()
        {
                       const int zmove = 0; //Forwards Backwards
            const int xmove = 1; //Left Right
            const int zxmove = 2; // All directions
            const int stoppls = 4;
            int mode = zmove;

            const int mechTalonid1 = 668;
            const int mechTalonid2 = 25;

            //const int meterTime = 100000000;

            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(0);
            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon2 = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(25);

//            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon3 = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(mechTalonid1);
  //          CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon4 = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(mechTalonid2);


            /*
            double read0;
            double read1;
            double read2;
            */

            /* create a gamepad object */
            CTRE.Phoenix.Controller.GameController myGamepad = new CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(0));

            CTRE.Phoenix.Controller.GameControllerValues gv = new CTRE.Phoenix.Controller.GameControllerValues();

            /* loop forever */
            //AUTON!!!!
            var startTime = DateTime.UtcNow;

            while (DateTime.UtcNow - startTime < TimeSpan.FromTicks(20500000))
            {
                myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 1);
                myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, -1);
                Debug.Print("Test: ");
                /* allow motor control */
                CTRE.Phoenix.Watchdog.Feed();
            }

            startTime = DateTime.UtcNow;

            while (DateTime.UtcNow - startTime < TimeSpan.FromTicks(1000000))
            {
                myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);
                myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);
                CTRE.Phoenix.Watchdog.Feed();
            }
            
            startTime = DateTime.UtcNow;

            while (DateTime.UtcNow - startTime < TimeSpan.FromTicks(1000000))
            {
                myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, -.5);
                myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 1);
            }


            while (true)
            {
                /*
                read0 = analogInput0.Read();
                read1 = analogInput1.Read();
                read2 = analogInput2.Read();
                */
                //Debug.Print("bruh");
                myGamepad.GetAllValues(ref gv);

                if (myGamepad.GetButton(1) == true) {
                    mode = zmove; //forwardsbackwards
                } else if(myGamepad.GetButton(2) == true) {
                    mode = xmove; //leftright
                } else if(myGamepad.GetButton(3) == true) {
                    mode = zxmove;
                } else if (myGamepad.GetButton(4) == true) {
                    mode = stoppls;
                }

                float speed = myGamepad.GetAxis(1) * -1;
                float turn = myGamepad.GetAxis(0) * 1;

                float shoulder = myGamepad.GetAxis(2);
                float elbow = myGamepad.GetAxis(5);

                    if (myGamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected)
                    {
                        /* print the axis values */
                        
                        Debug.Print("axis0 - LSH:" + myGamepad.GetAxis(0)); //Left Stick Horizontal // For movement of the wheels
                        Debug.Print("axis1 - LSV:" + myGamepad.GetAxis(1)); //Left Stick Vertical  //For movement of the wheels
                        Debug.Print("axis2 - RSH:" + myGamepad.GetAxis(2)); //Right Stick Horizontal //For movement of the Arm
                        Debug.Print("axis3 - RSV:" + myGamepad.GetAxis(5)); //Right Stick Vertical //For movement of the Arm
                        if (mode == zmove) {
                            Debug.Print("Forward/Reverse Mode");
                        } else if(mode == xmove) {
                            Debug.Print("Turning Mode");
                        } else if(mode == zxmove) {
                            Debug.Print("Forward/Reverse + Turning Mode");
                        } else if(mode == stoppls)
                        {
                            Debug.Print("STOP!!!");
                        }

                    /* allow motor control */
                    if (gv.pov == 1)
                    {
                        Debug.Print("reset");
                    }
                        

                    if (gv.pov == 2)
                        {
                            Debug.Print("p2");
                        }
                        else if (gv.pov == 3) 
                        {
                            Debug.Print("p3");
                        }
                        else if (gv.pov == 4)
                        {
                            Debug.Print("p4");
                        }
                        else if (gv.pov == 5) 
                        {
                            Debug.Print("p5");
                        }
                        else if (gv.pov == 6)
                        {
                            Debug.Print("p6");
                        }
                        else if (gv.pov == 7)
                        {
                            Debug.Print("p7");
                        } else if (gv.pov == 8)
                        {
                           Debug.Print("p8");
                        } else
                        {
                        }
                        
                        if (mode == zmove)
                        {
                            myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed * 1);
                            myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed * -1);
                            
                         //   myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, shoulder);
                           // myTalon4.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, elbow);
                        }
                        else if (mode == xmove)
                        {
                            myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn * 1);
                            myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn * 1);

                            //myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, shoulder);
                            //myTalon4.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, elbow);
                        }
                        else if (mode == zxmove)
                        {
                            myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, (speed * 1) / 2 + turn / 2);
                            myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, (speed * -1) / 2 + turn / 2);

                            //myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, shoulder);
                            //myTalon4.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, elbow);
                        } else if (mode == stoppls) {
                            myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);
                            myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);

                            //myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);
                            //myTalon4.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);

                        }
                }


                Thread.Sleep(10);

                    CTRE.Phoenix.Watchdog.Feed();
                }

            
        }
    }
}
