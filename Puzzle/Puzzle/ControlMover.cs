using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    using System.Runtime.InteropServices;
    public static class ControlMover
    {
        public static bool ChangeCursor { get; set; }
        public static bool AllowMove { get; set; }
        public static bool AllowResize { get; set; }
        public static bool BringToFront { get; set; }
        public static int ResizingMargin { get; set; }
        public static int MinSize { get; set; }
        public static GameOnField Owner { get; set; }

        private static Point startMouse;
        private static Point startLocation;
        private static Size startSize;
        private static bool resizing = false;
        static Cursor oldCursor;

        static ControlMover()
        {
            ResizingMargin = 5;
            MinSize = 10;
            ChangeCursor = false;
            AllowMove = true;
            AllowResize = true;
            BringToFront = true;
        }

        public static void Add(Control ctrl)
        {
            ctrl.MouseDown += ctrl_MouseDown;
            ctrl.MouseUp += ctrl_MouseUp;
            ctrl.MouseMove += ctrl_MouseMove;

        }
        //public static void AddM(Control ctrl)
        //{
        //    ctrl.MouseDown += ctrl_MouseDown;
        //    ctrl.MouseUp += ctrl_MouseUp;
        //    ctrl.MouseMove += ctrl_MouseMove;
        //}

        private static void ctrl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
       
            var ctrl = (sender as Control);
            ctrl.Cursor = oldCursor;
            Owner.setPieceIfOnRightLocation(sender);
        }

        public static void Remove(Control ctrl)
        {
            ctrl.MouseDown -= ctrl_MouseDown;
            ctrl.MouseUp -= ctrl_MouseUp;
            ctrl.MouseMove -= ctrl_MouseMove;
        }

        static void ctrl_MouseMove(object sender, MouseEventArgs e)
<<<<<<< HEAD
        {
            //if (select)
            //{
            //    var picBox = ((PicBox)sender);
            //    var x = e.X;
            //    var y = e.Y;
            //    int wight = picBox.Image.Width;
            //    int height = picBox.Image.Height;

            //    if (x > 0 && y > 0 && x < wight && y < height)
            //    {
            //        Color iii = ((Bitmap)picBox.Image).GetPixel(x, y);
            //        var is_this_top_piece = !(iii.ToArgb() == Color.Transparent.ToArgb());

            //        if (is_this_top_piece)
            //        {
                        var ctrl = sender as Control;

                        if (ChangeCursor)
                        {
                            if ((e.X >= ctrl.Width - ResizingMargin) && (e.Y >= ctrl.Height - ResizingMargin) && AllowResize)
                                ctrl.Cursor = Cursors.SizeNWSE;
                            else
                            if (AllowMove)
                                ctrl.Cursor = Cursors.SizeAll;
                            else
                                ctrl.Cursor = Cursors.Default;
                        }

                        if (e.Button != MouseButtons.Left)
                            return;

                        var l = ctrl.PointToScreen(e.Location);
                        var dx = l.X - startMouse.X;
                        var dy = l.Y - startMouse.Y;

                        if (Math.Max(Math.Abs(dx), Math.Abs(dy)) > 1)
                        {
                            if (resizing)
                            {
                                if (AllowResize)
                                {
                                    ctrl.Size = new Size(Math.Max(MinSize, startSize.Width + dx), Math.Max(MinSize, startSize.Height + dy));
                                    ctrl.Cursor = Cursors.SizeNWSE;
                                    if (BringToFront) ctrl.BringToFront();
                                }
                            }
                            else
                            {
                                if (AllowMove)
                                {
                                    Point newLoc = startLocation + new Size(dx, dy);
                                    if (newLoc.X < 0) newLoc = new Point(0, newLoc.Y);
                                    if (newLoc.Y < 0) newLoc = new Point(newLoc.X, 0);
                                    ctrl.Location = newLoc;
                                    ctrl.Cursor = Cursors.SizeAll;
                                    if (BringToFront) ctrl.BringToFront();
                                }
                            }
                            if (GameOnField.triangle)
                            {
                                ((PicBox)sender).Invalidate();
                            }
                        }
                //    }
                //    else
                //    {
                //        //var pb0 = (GetChildAtPoint(new Point(e.X, e.Y)));
                //        picBox.MouseMove -= ctrl_MouseMove;
                //        var pic= GameOnField.SelectPicBox(Cursor.Position.X, Cursor.Position.Y, picBox);

                //        pic.MouseDown += ctrl_MouseDown;
                //        pic.MouseUp += ctrl_MouseUp;
                //        pic.MouseMove += ctrl_MouseMove;
                //        //  GameOnField.SelectPicBox(Cursor.Position.X, Cursor.Position.Y, picBox);
                //    }
                //}
           // }
=======
        {            
            var ctrl = sender as Control;

            if (ChangeCursor)
            {
                if ((e.X >= ctrl.Width - ResizingMargin) && (e.Y >= ctrl.Height - ResizingMargin) && AllowResize)
                    ctrl.Cursor = Cursors.SizeNWSE;
                else
                if (AllowMove)
                    ctrl.Cursor = Cursors.SizeAll;
                else
                    ctrl.Cursor = Cursors.Default;
            }

            if (e.Button != MouseButtons.Left)
                return;

            var l = ctrl.PointToScreen(e.Location);
            var dx = l.X - startMouse.X;
            var dy = l.Y - startMouse.Y;

            //****
            PicBox pb = (PicBox)Owner.ifTransparentGetWhatIsUnder(sender, ((PicBox)sender).Location, e.Location);
            if (pb != null)
            {
                pb.BringToFront();
                if (!sender.Equals(pb))
                {
                    releaseLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
                    pressLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
                }
                else
                {

                }
            }
            else
            {
                releaseLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
                return;
            }
            //****

            if (Math.Max(Math.Abs(dx), Math.Abs(dy)) > 1)
            {
                if (resizing)
                {
                    if (AllowResize)
                    {
                        ctrl.Size = new Size(Math.Max(MinSize, startSize.Width + dx), Math.Max(MinSize, startSize.Height + dy));
                        ctrl.Cursor = Cursors.SizeNWSE;
                        if (BringToFront) ctrl.BringToFront();
                    }
                }
                else
                {
                    if (AllowMove)
                    {
                        Point newLoc = startLocation + new Size(dx, dy);
                        if (newLoc.X < 0) newLoc = new Point(0, newLoc.Y);
                        if (newLoc.Y < 0) newLoc = new Point(newLoc.X, 0);
                        ctrl.Location = newLoc;
                        ctrl.Cursor = Cursors.SizeAll;
                        if (BringToFront) ctrl.BringToFront();
                    }
                }
                if (GameOnField.triangle)
                {
                    ((PicBox)sender).Invalidate();
                }                
            }
>>>>>>> 6441c9e5c5e58f746bbddf5cf623b8d9eb55f26a
        }
       // static bool select = false;
        static void ctrl_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button != MouseButtons.Left)
                return;
<<<<<<< HEAD
         
            Owner.setOldLocation(sender);
            ((PicBox)sender).BringToFront();
            var ctrl = sender as Control;
=======

            PicBox pb = (PicBox)Owner.ifTransparentGetWhatIsUnder(sender, ((PicBox)sender).Location, e.Location);
            if (pb != null)
            {
                pb.BringToFront();
                Owner.setOldLocation(pb);
                var ctrl = pb as Control;

                resizing = (e.X >= ctrl.Width - ResizingMargin) && (e.Y >= ctrl.Height - ResizingMargin) && AllowResize;
                startSize = ctrl.Size;
                startMouse = ctrl.PointToScreen(e.Location);
                startLocation = ctrl.Location;
                oldCursor = ctrl.Cursor;
                if (!sender.Equals(pb))
                {
                    releaseLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
                    pressLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
                }
                else
                {
                    
                }
            }
            else
            {
                releaseLeftMouse((uint)e.Location.X, (uint)e.Location.Y);
            }
            //((PicBox)sender).BringToFront();
            
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
>>>>>>> 6441c9e5c5e58f746bbddf5cf623b8d9eb55f26a

        static void pressLeftMouse(uint dx, uint dy)
        {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, dx, dy, 0, 0);
        }
<<<<<<< HEAD
=======
        static void releaseLeftMouse(uint dx, uint dy)
        {
            mouse_event((uint)MouseEventFlags.LEFTUP, dx, dy, 0, 0);
        }
    }

    [Flags]
    public enum MouseEventFlags
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010
>>>>>>> 6441c9e5c5e58f746bbddf5cf623b8d9eb55f26a
    }
}


