using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
// I have documented this generally according to execution so i would strongly recommend that
//one starts reading this code from static void main and accordinally follow from there.
{
    class Program
    {
        
        
        private static void MoveWumpus(Cave[] CaveSystem, ref int[] Wumpus)
        {
            //Since  i am going to move the wumpus, i first turn the blood boolean to false in all the effected objects. I do this in the first for loop
            //Then i move the wumpus to a adjacent room using the next for loop. Then when the wumpus has moved i set the blood boolean to true for the relevant rooms. 
           for (int q = 0; q < 3; q++)
           {

               for (int s = 0; s < 3; s++)
               {
                   CaveSystem[CaveSystem[CaveSystem[Wumpus[0]].exit[q]].exit[s]].blood = false;


               }
           }
           for (int b = Wumpus[0], c =0; ((c < 3) && (Wumpus[0] == b)); c++)
           {
               Wumpus[0] = CaveSystem[b].exit[c];
           }
           
           for (int q = 0; q < 3; q++)
           {

               for (int s = 0; s < 3; s++)
               {
                   CaveSystem[CaveSystem[CaveSystem[Wumpus[0]].exit[q]].exit[s]].blood = true;


               }
           }
        }
        private static void BatEncounter(ref int HunterRoomNumber,  Cave[] CaveSystem, ref int BatTemperment,ref int MaxNoOfRooms,ref int RoomNumberBats,ref int[] Wumpus, ref int[] NumberRoomsPit,ref bool Game)
        {
            //This fn takes in a number of variables from static main. A random number c between 0 & BatTemperment is generated. BatTemperment starts at 4 in easy hen 3 & 4 for
            //ordinary and pro levels respectively. 'c' is evaluated on attaining a random between 0 and BatTemperment so as BatTemperment is reduced, the probability of
            //c hittting one increases. When it does the execution passes into the the first if statement; informing the player that they have disturbed the Bats.
            //The Hunter is then allocated a new room via the three argrument version od SetUpIndex. The next two if statements ascertain whether that rooom has a wumpus
            //or contains a pit whereby the player is informed of the sad story and Game is set to False.
            Random random = new Random();
            int c = random.Next(0, BatTemperment);
            //testpoint1-charlie. set c =1
            //c = 1;
            Console.WriteLine("You have entered a room of Bats");
            if(c == 1)
            {
                Console.WriteLine("Unfortunatley you disturbed the Bats, they are taking you to another room");
                HunterRoomNumber = SetUpIndex(ref HunterRoomNumber, ref MaxNoOfRooms,ref RoomNumberBats);
                //testpoint1-delta. move hunter to wumpus.
                //HunterRoomNumber = Wumpus[0];
            }
            if (CaveSystem[HunterRoomNumber].wumpus == true)
            {
                Console.WriteLine("You have been landed in a room with a wumpus, you are dead");
                Game = false;
            }
            if (CaveSystem[HunterRoomNumber].pit == true)
            {
                Console.WriteLine("You have been landed in a room with a pit, you are dead");
                Game = false;
            }
            if (c != 1)
            {
                Console.WriteLine("Thankfully you have not disturbed them, you may proceed");
            }
            
            
        }
        
        private static void DirectionOfArrows(ref string s,Cave[] CaveSystem,ref int a,ref int NoWumpi,ref int[] BloodRoomsTier1,ref int[] BloodRoomsTier2, ref int[] Wumpus, ref bool Game)
            {
                //If there is a wumpus in the room, the game is over, that is the purpose of the first if statement. If the effected room is adjacent to a room with
                //a wumpus then the wumpus is move using the Movewumpus fn in the last for loop. Please refer to this fn for explanation.
                //CaveSystem[a].arrow = true;
                
                if (CaveSystem[a].wumpus == true)
                {
                    CaveSystem[a].wumpus = false;
                    Console.WriteLine("One Wumpus killed");
                    NoWumpi--;
                    
                    if (NoWumpi == 0)
                    {
                        Console.WriteLine("Congratulations, all the wumpi are killed");
                        Game = false;
                    }
                }
                for(int z = 0; z < 3;z++)
                {
                    if(a == CaveSystem[Wumpus[0]].exit[z])
                    {
                        MoveWumpus(CaveSystem, ref  Wumpus);
                    }
                }
            }
        private static void ShootArrows(ref int NoArrows, Cave[] CaveSystem,ref int HunterRoomNumber,ref int NoWumpi,ref int[] BloodRoomsTier1,ref int[] BloodRoomsTier2, ref int[] Wumpus, ref bool Game)
        {
            // This fn starts by decrementing the number of arrows and informing the player if their are none left. Then the player is asked for the direction
            // that the arrows will take. So i ask the player to enter the rooms that arrow should fire or proceed through. I let the player choose l,r or c for left,right
            //and centre respectively.I do this for the first room, then the second rooma nd finally the third room. The input is put into three strings a,b & c.
            //I then have a switch statement with case in turn having a couple of nested if statements. I switch 'a', if it is 'l', then i set d to be the first exit of that
            //room CaveSystem[HunterRoomNumber].exit[0]. I then set string s = 'l' and call the fn DirectionOfArrows. Please refer to this fn now. I then nased on the value of
            //string a determine b, i reassign d == CaveSystem[d].exit[0] to the next room where exit[0] refers to 'l' or left. or exit[1] for c and finally exit[2] for 'r'.
            int d = 0;
            string s = "";
            NoArrows = NoArrows - 1;
            if (NoArrows == 0)
            {
                Console.WriteLine("Last arrow fired");
            }
            Console.WriteLine("Please chose which room to fire your Arrow");
            Console.WriteLine("For the room on the left , press l for left, r for right, c for centre.");
            string a = Console.ReadLine();
            Console.WriteLine("Now choose the the next room that the arrow enters upon entering that room: l for , r for right or c for centre");
            string b = Console.ReadLine();
            Console.WriteLine("Now the final room that the arrow enters upon entering that room: l for , r for right or c for centre");
            string c = Console.ReadLine();
            switch(a)
            {
                case "l":
                    d= CaveSystem[HunterRoomNumber].exit[0];
                    s  ="l";
                    DirectionOfArrows(ref s,  CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus,ref Game);
                    if(b == "l")
                    {
                        d = CaveSystem[d].exit[0]; s = "l";
                        DirectionOfArrows(ref  s, CaveSystem, ref d, ref NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref s, CaveSystem, ref d, ref NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref s, CaveSystem, ref d, ref NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "r")
                    {
                        d = CaveSystem[d].exit[2]; s = "r";
                        DirectionOfArrows(ref s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "c")
                    {
                        d = CaveSystem[d].exit[1];
                        s = "c";
                        DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    break;
                case "r":
                    d= CaveSystem[HunterRoomNumber].exit[2];s ="r";
                    DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                    if(b == "l")
                    {
                        d = CaveSystem[d].exit[0]; s = "l";
                        DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref s, CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "r")
                    {
                        d = CaveSystem[d].exit[2]; s = "r";
                        DirectionOfArrows(ref s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref s, CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "c")
                    {
                        d = CaveSystem[d].exit[1]; s = "c";
                        DirectionOfArrows(ref s, CaveSystem, ref d, ref NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref s, CaveSystem, ref d, ref NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    break;
                case "c":
                    d= CaveSystem[HunterRoomNumber].exit[1];s ="l";
                    DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                    if(b == "l")
                    {
                        d = CaveSystem[d].exit[0]; s = "l";
                        DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "r")
                    {
                        d = CaveSystem[d].exit[2]; s = "r";
                        DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    else if(b == "c")
                    {
                        d = CaveSystem[d].exit[1]; s = "c";
                        DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        if(c =="l")
                        {
                            d = CaveSystem[d].exit[0]; s = "l";
                            DirectionOfArrows(ref  s, CaveSystem, ref d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="r")
                        {
                            d = CaveSystem[d].exit[2]; s = "r";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                        else if( c =="c")
                        {
                            d = CaveSystem[d].exit[1]; s = "c";
                            DirectionOfArrows(ref  s, CaveSystem, ref  d, ref  NoWumpi, ref BloodRoomsTier1, ref BloodRoomsTier2, ref Wumpus, ref Game);
                        }
                    }
                    break;
                default:
                    break;
                            
              
            }
            
        }

        
        private static int SetUpIndex(ref int a, ref int MaxNoOfRooms,ref int RoomNumberBats,ref int[] Wumpus, ref int[] NumberRoomsPit)
        {
            //This fn will return a room number using a random generator provided it does not equal any the argruments it is compared against. By way of reference it will
            //be passed the max number of rooms, the room numbers that contain the bats,wumpi and pits. "a" usually is usually the room of the hunter. "c" will return
            //a value within the range of the number of caves. I do this in a do while loop which keeps going if  c generated is the same as 'a' or either room with Bats , wumpi
            //or pits. There are two versions of this function. This version takes five argruments and compares all in the while statement. Howver i als have a version
            //which only takes three argruments which is used in the BatEncounter fn.
            int c = 0;
            do
            {
                Random random = new Random();
                c = random.Next(0, MaxNoOfRooms);
 

            }while (c == a||c==RoomNumberBats||c== Wumpus[0]|| c==NumberRoomsPit[0]||c==NumberRoomsPit[1]||c == Wumpus[1]);
            return c;
        }
        private static int SetUpIndex(ref int a, ref int MaxNoOfRooms, ref int RoomNumberBats)
        {
           //This version od SetUpIndex takes three argruments. It is called by the BatEncounter fn and allocates the hunter to another room besides its current one.
            int c = 0;
            do
            {
                Random random = new Random();
                c = random.Next(0, MaxNoOfRooms);


            } while (c == a || c == RoomNumberBats);
            return c;
        }
        static void Main(string[] args)
        {
            //This is where the program starts. Game is a boolean which i use in a later while loop. When the hunter is killed , it is turned to false which causes the
            //game to finish.Index,index1 are variables i use later in the program.
            //NoWumpi specify the number of wumpi in the maze. Currently set to one.NoArrows is the number arrows available to shoot which is initially set to twice the
            //number of wumpi, i.e. 2.The variables a,b, & c are used in the cave class which i will explain later. MaxNoofrooms is by default set to 36 which are the number
            //of rooms or caves for the easy level, which is the default level.
            bool Game = true;
            int index=6, index1=7;
            int NoWumpi = 2;
            int NoArrows = 2 * NoWumpi;
            int a = 0, b = 0, c = 0;
            int MaxNoOfRooms =36;
            //Please refer to the Cave class setup after class program. I now setup an array CaveSystem of objects of class Cave. the default id 36 rooms for easy.
            Cave[] CaveSystem = new Cave[MaxNoOfRooms];
            //This variable refers to the temper of the Bats. The higher the value the less lightly the bats will remove the hunter to another room if disturbed.
            int BatTemperment = 4;

          
            Console.WriteLine(" Welcome to wumpus, there are three levels: Easy, Ordinary and Pro;\n Easy is thirty six  caves, one Wumpus and one Colony of Bats;\n Ordinary is twenty eight caves, one wumpus and one Colony of Bats\n Hard is sixteen caves, 1 Colony of Bats & one Wunpus;\n The bats get more aggressive as the levels progress\n Enter o & return for ordinary while p for pro; default is Easy");
            //I use the console application to output a introduction and receive input from the player. 
            string input = Console.ReadLine();
            // I red the user input. Depending on the input i declare the which maze i am going to use . The default is easy if the player simply presses return.
            if (input == "p")
            {
                BatTemperment = 2;
                MaxNoOfRooms = 15;
                CaveSystem[0] = new Cave(a = 1, b = 7, c = 8);
                CaveSystem[1] = new Cave(a = 0, b = 2, c = 9);
                CaveSystem[2] = new Cave(a = 1, b = 3, c = 10);
                CaveSystem[3] = new Cave(a = 2, b = 4, c = 11);
                CaveSystem[4] = new Cave(a = 3, b = 5, c = 12);
                CaveSystem[5] = new Cave(a = 4, b = 6, c = 13);
                CaveSystem[6] = new Cave(a = 5, b = 7, c = 14);
                CaveSystem[7] = new Cave(a = 0, b = 6, c = 15);
                CaveSystem[8] = new Cave(a = 0, b = 9, c = 15);
                CaveSystem[9] = new Cave(a = 1, b = 8, c = 10);
                CaveSystem[10] = new Cave(a = 2, b = 9, c = 11);
                CaveSystem[11] = new Cave(a = 3, b = 10, c = 12);
                CaveSystem[12] = new Cave(a = 4, b = 11, c = 13);
                CaveSystem[13] = new Cave(a = 5, b = 12, c = 14);
                CaveSystem[14] = new Cave(a = 6, b = 13, c = 15);
                CaveSystem[15] = new Cave(a = 7, b = 8, c = 14);
            }
            else if (input == "o")
            {
                MaxNoOfRooms = 27;
                BatTemperment = 3;
                //Cave[] CaveSystem = new Cave[NoOfRooms];
                CaveSystem[0] = new Cave(a = 1, b = 7, c = 8);
                CaveSystem[1] = new Cave(a = 0, b = 2, c = 9);
                CaveSystem[2] = new Cave(a = 1, b = 3, c = 10);
                CaveSystem[3] = new Cave(a = 2, b = 4, c = 11);
                CaveSystem[4] = new Cave(a = 3, b = 5, c = 12);
                CaveSystem[5] = new Cave(a = 4, b = 6, c = 13);
                CaveSystem[6] = new Cave(a = 5, b = 7, c = 14);
                CaveSystem[7] = new Cave(a = 0, b = 6, c = 15);
                CaveSystem[8] = new Cave(a = 0, b = 16, c = 21);
                CaveSystem[9] = new Cave(a = 1, b = 16, c = 17);
                CaveSystem[10] = new Cave(a = 2, b = 11, c = 17);
                CaveSystem[11] = new Cave(a = 3, b = 10, c = 18);
                CaveSystem[12] = new Cave(a = 4, b = 18, c = 19);
                CaveSystem[13] = new Cave(a = 5, b = 14, c = 19);
                CaveSystem[14] = new Cave(a = 6, b = 13, c = 20);
                CaveSystem[15] = new Cave(a = 7, b = 20, c = 21);
                CaveSystem[16] = new Cave(a = 8, b = 9, c = 22);
                CaveSystem[17] = new Cave(a = 9, b = 10, c = 23);
                CaveSystem[18] = new Cave(a = 3, b = 11, c = 12);
                CaveSystem[19] = new Cave(a = 12, b = 13, c = 25);
                CaveSystem[20] = new Cave(a = 14, b = 15, c = 26);
                CaveSystem[21] = new Cave(a = 8, b = 15, c = 27);
                CaveSystem[22] = new Cave(a = 16, b = 23, c = 27);
                CaveSystem[23] = new Cave(a = 17, b = 22, c = 24);
                CaveSystem[24] = new Cave(a = 18, b = 23, c = 25);
                CaveSystem[25] = new Cave(a = 19, b = 24, c = 26);
                CaveSystem[26] = new Cave(a = 20, b = 25, c = 27);
                CaveSystem[27] = new Cave(a = 21, b = 22, c = 26);
                
            }
                
                //Cave[] CaveSystem = new Cave[NoOfRooms];
                CaveSystem[0] = new Cave(a = 1, b = 7, c = 8);
                CaveSystem[1] = new Cave(a = 0, b = 2, c = 9);
                CaveSystem[2] = new Cave(a = 1, b = 3, c = 10);
                CaveSystem[3] = new Cave(a = 2, b = 4, c = 11);
                CaveSystem[4] = new Cave(a = 3, b = 5, c = 12);
                CaveSystem[5] = new Cave(a = 4, b = 6, c = 13);
                CaveSystem[6] = new Cave(a = 5, b = 7, c = 14);
                CaveSystem[7] = new Cave(a = 0, b = 6, c = 15);
                CaveSystem[8] = new Cave(a = 0, b = 16, c = 21);
                CaveSystem[9] = new Cave(a = 1, b = 16, c = 17);
                CaveSystem[10] = new Cave(a = 2, b = 11, c = 17);
                CaveSystem[11] = new Cave(a = 3, b = 10, c = 18);
                CaveSystem[12] = new Cave(a = 4, b = 18, c = 19);
                CaveSystem[13] = new Cave(a = 5, b = 14, c = 19);
                CaveSystem[14] = new Cave(a = 6, b = 13, c = 20);
                CaveSystem[15] = new Cave(a = 7, b = 20, c = 21);
                CaveSystem[16] = new Cave(a = 8, b = 9, c = 22);
                CaveSystem[17] = new Cave(a = 9, b = 10, c = 23);
                CaveSystem[18] = new Cave(a = 3, b = 11, c = 12);
                CaveSystem[19] = new Cave(a = 12, b = 13, c = 25);
                CaveSystem[20] = new Cave(a = 14, b = 15, c = 26);
                CaveSystem[21] = new Cave(a = 8, b = 15, c = 27);
                CaveSystem[22] = new Cave(a = 16, b = 28, c = 29);
                CaveSystem[23] = new Cave(a = 17, b = 24, c = 29);
                CaveSystem[24] = new Cave(a = 18, b = 23, c = 30);
                CaveSystem[25] = new Cave(a = 19, b = 26, c = 30);
                CaveSystem[26] = new Cave(a = 20, b = 25, c = 31);
                CaveSystem[27] = new Cave(a = 21, b = 31, c = 28);
                CaveSystem[28] = new Cave(a = 22, b = 27, c = 32);
                CaveSystem[29] = new Cave(a = 22, b = 23, c = 33);
                CaveSystem[30] = new Cave(a = 24, b = 25, c = 34);
                CaveSystem[31] = new Cave(a = 26, b = 27, c = 35);
                CaveSystem[32] = new Cave(a = 28, b = 33, c = 35);
                CaveSystem[33] = new Cave(a = 29, b = 32, c = 34);
                CaveSystem[34] = new Cave(a = 30, b = 33, c = 35);
                CaveSystem[35] = new Cave(a = 31, b = 32, c = 34);

            //I initially place the hunter in room 6,HunterRoomNumber = 6; the bats in room 7. Please note i set the boolean for each object to true. So for room or cave 6
            //i reference that cave vis CaveSystem ans set the booleanHunter = true. Likewise for the bats. 
            
            int HunterRoomNumber = 6;
            CaveSystem[HunterRoomNumber].Hunter = true;
            int RoomNumberBats = 7;
            CaveSystem[RoomNumberBats].bat = true;
            //I use an array NumberRoomsPit = new int[2] which the contains the room numbers of the rooms with a pit in the game. BloodRoomsTier1 & 2 are arrays i set up
            //previously but are now reduntant.Wumpus is an array which contains the room numbers of the wumpi, however i only have one wumpus in the game as set out in
            //the spec; so i just use Wumpus[0]; however i set all the code and functions to use the array so i just left it as is.
            int[] NumberRoomsPit = new int[2];
            int[] BloodRoomsTier1 = new int[3];
            int[] BloodRoomsTier2 = new int[3];
            int[] Wumpus = new int[2];
            //I now put the wumpus into a room. I use a function called SetUpIndex. Please refer to this fn for explanation.I then set the boolean for the wumpus for that cave
            //to true.
            //Wumpus[0] = SetUpIndex(ref HunterRoomNumber, ref MaxNoOfRooms,ref RoomNumberBats,ref Wumpus, ref NumberRoomsPit);
            //CaveSystem[Wumpus[0]].wumpus = true;
            //This for loop will setup the rooms with pits and set the corresponding boolean for the relevant object. Also i have another for loop inside which sets 
            //the slime boolean for each relevant object. I do this by referencing the the exit property of each object and then referencing the slime boolean for the
            //corresponding room. Firstly i attain the rooms adjacent to the pits: CaveSystem[NumberRoomsPit[0]].exit[q] and i change their boolean slime:
            //CaveSystem[CaveSystem[NumberRoomsPit[0]].exit[q]].slime = true;
            for (int i = 0; i < 2; i++)
            {
                NumberRoomsPit[i] = SetUpIndex(ref index, ref MaxNoOfRooms,ref RoomNumberBats,ref Wumpus, ref NumberRoomsPit);
                Wumpus[i] = SetUpIndex(ref HunterRoomNumber, ref MaxNoOfRooms, ref RoomNumberBats, ref Wumpus, ref NumberRoomsPit);
                CaveSystem[Wumpus[i]].wumpus = true;
                
                //NumberRoomsPit[0] = CaveSystem[HunterRoomNumber].exit[0];
                CaveSystem[NumberRoomsPit[i]].pit = true;
                //CaveSystem[Wumpus[i]].wumpus = true;
                for (int q = 0; q < (CaveSystem[NumberRoomsPit[i]].exit.Length); q++)
                {
                    CaveSystem[CaveSystem[NumberRoomsPit[0]].exit[q]].slime = true;
                    CaveSystem[CaveSystem[NumberRoomsPit[1]].exit[q]].slime = true;
                }
         
            }
            //This for loop will identify the rooms two rooms adjacent to a room containing a wumpus and set the boolean blood to true. There are actually two for loops.
            //The first for loop goes through each room adjacent to a wumpus while the second for loop goes through each room adjacent to that room. Again this is
            //accomplished by first getting rooms adjacent to the wumpus: CaveSystem[Wumpus[0]].exit[q] and getting the rooms adjacent to these:CaveSystem[CaveSystem[Wumpus[0]].exit[q]].exit[s]
            // and finally accessing the boolean  blood of these rooms: CaveSystem[CaveSystem[CaveSystem[Wumpus[0]].exit[q]].exit[s]].blood = true;
            for (int q = 0; q < 3; q++)
            {

                for (int s = 0; s < 3; s++)
                {
                    CaveSystem[CaveSystem[CaveSystem[Wumpus[0]].exit[q]].exit[s]].blood = true;


                }
            }
            //This while loop runs for the duration of the game. When Game is set to false, the game terminates.
            while (Game == true)
            {
                //This for loop checks the status of the rooms adjacent to the hunter. If the booleans for blood, slime,wumpus and bats are true then the player is informed
                //that they can smell the wumpus,hear bats, detect slime or as in the case of blood, the wumpus is two rooms away.
                for (int i = 0; i < CaveSystem[HunterRoomNumber].exit.Length;i++ )
                {
                    if(CaveSystem[CaveSystem[HunterRoomNumber].exit[i]].bat == true)
                    {
                        Console.WriteLine("You can hear bats");
                    }
                    if (CaveSystem[CaveSystem[HunterRoomNumber].exit[i]].slime == true)
                    {
                        Console.WriteLine("You can hear the wind wistling and there is slime on the floor");
                    }
                    if (CaveSystem[CaveSystem[HunterRoomNumber].exit[i]].blood == true)
                    {
                        Console.WriteLine("There is blood on the floor");
                    }
                    if (CaveSystem[CaveSystem[HunterRoomNumber].exit[i]].wumpus == true)
                    {
                        Console.WriteLine("You can smell a Wumpus");
                    }
                }
                //The player is asked what he or she wants to do. Press 1 & enter to omove or 2 & enter to shoot.
                Console.WriteLine("What would like to do?");
                Console.WriteLine("Press 1 and return to move");
                if (NoArrows > 0)
                {
                    Console.WriteLine(" or press 2 and return to shoot");
                }
                string s = Console.ReadLine();
                if(s == "1")
                {
                    //The hunter moves to a new room using a random selection of the three adjacent rooms.The first three if statements ascertain whether the room the
                    //hunter is now in has a pit, a wumpus or indeed a colony of bats.
                    Random random = new Random();
                    HunterRoomNumber = CaveSystem[HunterRoomNumber].exit[random.Next(0, 2)];
                    HunterRoomNumber = SetUpIndex(ref HunterRoomNumber, ref MaxNoOfRooms,ref RoomNumberBats,ref Wumpus, ref NumberRoomsPit);
                    //testpoint1- beta. to test encounter with bats.
                    //HunterRoomNumber = RoomNumberBats;
                    //HunterRoomNumber = NumberRoomPits[0];
                    //HunterRoomNumber = Wumpus[0];
                    if (CaveSystem[HunterRoomNumber].pit == true && CaveSystem[HunterRoomNumber].wumpus == true)
                    {
                        Console.WriteLine("You have encountered a pit and a Wumpus, you are dead, Game Over");
                        Game = false;
                    }
                    
                    if (CaveSystem[HunterRoomNumber].pit == true)
                    {
                        Console.WriteLine("You have encountered a pit, you are dead, Game Over");
                        Game = false;
                    }
                    if (CaveSystem[HunterRoomNumber].wumpus == true)
                    {
                        Console.WriteLine("You have encountered a Wumpus, you are dead, Game Over");
                        Game = false;
                    }
                    //HunterRoomNumber = RoomNumberBats;
                    if (CaveSystem[HunterRoomNumber].bat == true)
                    {
                        //If there are bats in this room then the BatEncounter fn is called. Please refer to this fn for explanation.
                        BatEncounter(ref HunterRoomNumber, CaveSystem, ref BatTemperment, ref MaxNoOfRooms, ref RoomNumberBats, ref Wumpus, ref NumberRoomsPit, ref Game);
                    }
                }
                else if(s == "2" && NoArrows > 0)
                {
                    //If the Player chooses to allow the hunter to fire an arrow they execution follows the said fn. Please refer to this fn for clarification
                    ShootArrows(ref NoArrows, CaveSystem,ref  HunterRoomNumber,ref  NoWumpi, ref BloodRoomsTier1,ref BloodRoomsTier2,ref Wumpus,ref Game);
                }
                        

                
            }
            Console.WriteLine("Game Over");
    
        }
        
        
    }
    public class Cave
    {
        //This is my class cave which i use for the individual caves or rooms in the maze. I have two constructors because initally because in my initial design some
        //of my rooms had only two rooms that they connected too in some instances. The exit array specifies the room numbers that each room connects to. Their purpose
        //will be shown once i return to main. Finally there is also a list of bolleans associated with each object: pit,slime,blood,wumpus,hunter, bat and arrow which indicates
        // if any these objects are in that cave or in anothe rwords associated with that cave then the boolean is turned to true.
        public int[] exit = new int[3];

        public Cave(int a, int b, int c)
        {
            this.exit[0] = a;
            this.exit[1] = b;
            this.exit[2] = c;
        }
        public Cave(int a, int b)
        {
            this.exit[0] = a;
            this.exit[1] = b;
            //this.exit[2] = c;
        }
        public bool pit = false;
        public bool slime = false;
        public bool bat = false;
        public bool Hunter = false;
        public bool wumpus = false;
        public bool blood = false;
        public bool arrow = false;
    }
}
