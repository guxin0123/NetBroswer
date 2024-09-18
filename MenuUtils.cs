using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NetBroswer
{
    internal class MenuUtils
    {


        #region GetSystemMenu 获取系统菜单
        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        #endregion

        #region AppendMenu 追加菜单项
        /// <summary>
        /// 追加菜单项
        /// </summary>
        /// <param name="hMenu">菜单指针</param>
        /// <param name="uFlags"></param>
        /// <param name="uIDNewItem"></param>
        /// <param name="lpNewItem"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);
        #endregion

        #region InsertMenu
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <param name="uIDNewItem"></param>
        /// <param name="lpNewItem"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);
        #endregion

        #region ModifyMenu
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <param name="uIDNewItem"></param>
        /// <param name="lpNewItem"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ModifyMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);
        #endregion

        #region CheckMenuItem
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <param name="uIDNewItem"></param>
        /// <param name="lpNewItem"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CheckMenuItem(IntPtr hMenu, int uIDCheckItem, int uCheck);
        #endregion


        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);


        [DllImport("user32.dll")]
        public static extern int GetMenuItemID(IntPtr hMenu,int nPos);



    }
}
