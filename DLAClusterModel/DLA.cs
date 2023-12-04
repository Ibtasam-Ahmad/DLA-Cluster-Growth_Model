using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLAClusterModel
{
    class DLA
    {
        // Decleare Data Variables
        int size = 500;
        //int pszie = 4;
        int[,] cluster; // 2 Dimentional Region, Represents the cluster region
        float x0 = 0, y0 = 0, dx, dy; // Center Coordinates of TextBox1
        Random obj;
        double prob;
        int x, y;
        bool check = false;
        
        // Graphical Setup
        Graphics gg;
        SolidBrush bb, br;
        Pen bl;

        // Now defining Constructor of the Class
        public DLA(Form1 frm, int flag)
        {
            gg = frm.textBox1.CreateGraphics();
            //gg = frm.CreateGraphics();

            x0 = (float)frm.textBox1.Width/2;
            y0 = (float)frm.textBox1.Height/2;
            //x0 = (float)frm.Width / 2;
            //y0 = (float)frm.Height / 2;
            // By this coordinates of the ceter are find

            dx = (float)frm.textBox1.Width/(2 * size);
            dy = (float)frm.textBox1.Height/(2 * size);
            //dx = (float)frm.Width / (2 * size);
            //dy = (float)frm.Height / (2 * size);

            bb = new SolidBrush(Color.Blue);
            br = new SolidBrush(Color.Red);
            bl = new Pen(Color.Black);

            obj = new Random();

            cluster = new int[2 * size + 1, 2 * size + 1];
            // odd number of rows & odd number of columns
            //By this the center can be find easily

            // Initialization of the CLuster Class Array/Region
            for (int i = 0; i < (2 * size + 1); i++)
            {
                for (int j = 0; j < (2 * size + 1); j++)
                {
                    cluster[i, j] = 0;
                    gg.DrawEllipse(bl, j * dx, i * dy, 5, 5);
                    if(flag == 0)
                    {
                        if (i == size && j == size)
                        {
                            cluster[i, j] = 1; // Seed has been Placed at center
                                               // Seed has placed at the center of the grid
                            gg.FillEllipse(br, j * dx, i * dy, 5, 5);
                        } // Seed has been Placed
                    }
                    if (flag == 1)
                    {
                        if (i == size)
                        {
                            cluster[i, j] = 1; // Seed has been Placed at x axis
                                               // Seed has placed at the center of the grid
                            gg.FillEllipse(br, j * dx, i * dy, 5, 5);
                        } // Seed has been Placed
                    }
                    if (flag == 2)
                    {
                        if (j == size)
                        {
                            cluster[i, j] = 1; // Seed has been Placed at y axis
                                               // Seed has placed at the center of the grid
                            gg.FillEllipse(br, j * dx, i * dy, 5, 5);
                        } // Seed has been Placed
                    }
                    if (flag == 3)
                    {
                        int temp1 = i - size;
                        int temp2 = j - size;
                        if (Math.Abs((temp1 * temp1) + (temp2 * temp2) - 1000) < 0.1)
                        {
                            cluster[i, j] = 1; // Seed has been Placed at center
                                               // Seed has placed at the center of the grid
                            gg.FillEllipse(br, j * dx, i * dy, 5, 5);
                        } // Seed has been Placed
                    }

                }
            }
        }
        // End of CLuster
        // Now Defining other functions

        public void Growth(bool filled)
        {
            while (!filled)
            {
                for (int i = 1; i < (2 * size); i++)
                {
                    for (int j = 1; j < (2 * size); j++)
                    {
                        x = obj.Next(1, 2 * size);
                        y = obj.Next(1, 2 * size);
                        if (cluster[x, y] == 0)
                        {
                            //filled = false;
                            if (decide(x, y))
                            {
                                cluster[x, y] = 1;
                                //Thread.Sleep(1);
                                gg.FillEllipse(br, y * dx, x * dy, 5, 5);
                            }
                            else
                            {
                                while (!decide(x, y))
                                {
                                    prob = obj.NextDouble();
                                    if (prob <= 0.25)
                                    {
                                        if (x < 2 * size - 1)
                                        {
                                            x++;
                                        }
                                    }
                                    if (prob > 0.25 && prob <= 0.5)
                                    {
                                        if (x > 1)
                                        {
                                            x--;
                                        }
                                    }
                                    if (prob > 0.5 && prob <= 0.75)
                                    {
                                        if (y < 2 * size - 1)
                                        {
                                            y++;
                                        }
                                    }
                                    if (prob > 0.75)
                                    {
                                        if (y > 1)
                                        {
                                            y--;
                                        }
                                    }
                                }
                             cluster[x, y] = 1;
                             //Thread.Sleep(1);
                             gg.FillEllipse(br, y * dx, x * dy, 4, 4);
                            }
                        }
                    }
                }
            }
        }

        // End of Growth Function
        public bool decide(int x, int y)
            // To decide ka koi nearest site occupied ha k nhi 
        {
            if(cluster[x - 1, y] == 1)
            {
                check = true;
            }
            else if (cluster[x + 1, y] == 1)
            {
                check = true;
            }
            else if (cluster[x, y - 1] == 1)
            {
                check = true;
            }
            else if (cluster[x, y + 1] == 1)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }
    }
}
