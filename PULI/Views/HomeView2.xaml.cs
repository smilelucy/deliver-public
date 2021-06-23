using PULI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PULI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView2 : BottomTabPage
    {
        public HomeView2()
        {
            InitializeComponent();
            Messager();
        }
        private void Messager()
        {
            MessagingCenter.Send(this, "BEACON_SCAN", true);
            Console.WriteLine("BEACONSCAN");
            Console.WriteLine("AUTH~~~" + MainPage.AUTH);
            MessagingCenter.Subscribe<MainPage, bool>(this, "NewDayDelete", (sender, arg) =>
            {
                if (arg)
                {
                    Console.WriteLine("newdayrecieve~homeview2~");
                    MapView.AccDatabase.DeleteAll();
                    MapView.PunchDatabase.DeleteAll();
                    MapView.PunchDatabase2.DeleteAll();
                    MapView.PunchTmp.DeleteAll();
                    MapView.PunchTmp2.DeleteAll();
                    MapView.PunchYN.DeleteAll();
                    MapView.name_list_in.Clear();
                    MapView.name_list_out.Clear();
                }

            });
            if (MainPage.AUTH == "4")
            {
                MessagingCenter.Send(this, "SET_MAP", true); // 傳送"UPDATE_BONUS"的指令給訂閱者(Subscribe)
                Console.WriteLine("SETMAP_4");
                //if (MainPage.totalList.daily_shipments.Count != 0)
                //{
                //    MessagingCenter.Send(this, "SET_MAP", true); // 傳送"UPDATE_BONUS"的指令給訂閱者(Subscribe)
                //    Console.WriteLine("SETMAP_4");
                //}
                //else
                //{
                //    Console.WriteLine("SETMAP_4_NOdata");
                //}
            }
            else
            {
                if (MainPage.allclientList.Count() != 0)
                {
                    MessagingCenter.Send(this, "SET_MAP", true); // 傳送"UPDATE_BONUS"的指令給訂閱者(Subscribe)
                    Console.WriteLine("SETMAP_6");
                }
            }
            if (MainPage.AUTH == "4")
            {
                MessagingCenter.Send(this, "SET_FORM", true);
                Console.WriteLine("SETFORM");
                //if (MainPage.totalList.daily_shipments.Count != 0)
                //{
                //    MessagingCenter.Send(this, "SET_FORM", true);
                //    Console.WriteLine("SETFORM");
                //}
            }
            //else
            //{
            //    if (MainPage.userList.daily_shipment_nums > 0)
            //    {
            //        MessagingCenter.Send(this, "SET_FORM", true);
            //        Console.WriteLine("SETFORM"); // for外送員的回饋單
            //    }
            //}

            if (MainPage.AUTH == "4")
            {
                //MessagingCenter.Send(this, "SET_SHIPMENT_FORM", true);
                //Console.WriteLine("SETSHIPMENT");
                if (MainPage.totalList.daily_shipments.Count != 0)
                {
                    MessagingCenter.Send(this, "SET_SHIPMENT_FORM", true);
                    Console.WriteLine("SETSHIPMENT");
                }
            }
            //else
            //{
            //    if (MainPage.userList.daily_shipment_nums > 0)
            //    {
            //        MessagingCenter.Send(this, "SET_SHIPMENT_FORM", true); // for社工總表
            //        Console.WriteLine("SETSHIPMENT_6");
            //    }
            //}

            if (MainPage.AUTH == "4")
            {
                if(MainPage.totalList.abnormals != null)
                {
                    if (MainPage.totalList.abnormals.Count != 0)
                    {
                        MessagingCenter.Send(this, "SET_CHANGE_FORM", true);
                        Console.WriteLine("CHANGE");
                    }
                    else
                    {
                        Console.WriteLine("CHANGE_NO");
                    }
                }
                
            }
            //else
            //{
            //    if (MainPage.userList.daily_shipment_nums > 0)
            //    {
            //        MessagingCenter.Send(this, "SET_CHANGE_FORM", true);
            //        Console.WriteLine("CHANGE_6"); // for社工的異動表
            //    }
            //}
            if (MainPage.AUTH == "6")
            {
                MessagingCenter.Send(this, "SET_AddCln_FORM", true);
                //MessagingCenter.Send(this, "SET_AddCln_FORM", true);
                Console.WriteLine("AddCln");
            }
            MessagingCenter.Subscribe<MainPage, bool>(this, "Deletesetnum", (sender, arg) =>
            {
                // do something when the msg "UPDATE_BONUS" is recieved
                if (arg)
                {
                    Console.WriteLine("Deletesetnum~~homeview2~~~");
                    MapView.PunchDatabase2.DeleteAll();
                }
            });
            MessagingCenter.Subscribe<MemberView, bool>(this, "OUT", (sender, arg) =>
            {
                if (arg)
                {
                    Navigation.PopModalAsync();

                }
            });

        }
    }
}