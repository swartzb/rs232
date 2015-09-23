using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceLib
{
    public class CustomResources
    {
        public static ComponentResourceKey styBorder
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "brdrStyle");
                return crk;
            }
        }

        public static ComponentResourceKey styLabel
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "lblStyle");
                return crk;
            }
        }

        public static ComponentResourceKey styListBox
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "lbStyle");
                return crk;
            }
        }

        public static ComponentResourceKey styListBoxItem
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "lbiStyle");
                return crk;
            }
        }

        public static ComponentResourceKey styButton
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "btnStyle");
                return crk;
            }
        }

        public static ComponentResourceKey styTextBlock
        {
            get
            {
                ComponentResourceKey crk = new ComponentResourceKey(
                    typeof(CustomResources), "tbkStyle");
                return crk;
            }
        }
    }
}
