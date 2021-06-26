using Deliver.Services;
using PULI.Model;
using PULI.Models.DataInfo;
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
    public partial class TestView : ContentPage
    {
        public static List<questionnaire> questionnaireslist = new List<questionnaire>();
        WebService web = new WebService();
        public static List<checkInfo> checkList = new List<checkInfo>();
        public static List<checkInfo> checkList2 = new List<checkInfo>();
        public static List<checkInfo> checkListtmp = new List<checkInfo>();
        public static List<checkInfo> checkListupload = new List<checkInfo>();
        //public static List<checkInfo> postcheck_list = new List<checkInfo>();
        //public static checkInfo check = new checkInfo();\
        checkInfo check = new checkInfo();
        Entry entny = new Entry();
        CheckBox check_box = new CheckBox();
        Label label_check = new Label();
        string ans;
        public static bool ischeck;
        public Dictionary<string, bool> TmpCheckList = new Dictionary<string, bool>();
        public Dictionary<string, string> TmpAnsList = new Dictionary<string, string>();
        public Dictionary<string, bool> TmpAdd_elseList = new Dictionary<string, bool>();
        public Dictionary<string, bool> TmpAddList = new Dictionary<string, bool>();
        public Dictionary<string, bool> CheckboxList = new Dictionary<string, bool>();
        public Dictionary<string, bool> IsResetList = new Dictionary<string, bool>();
        public Dictionary<string, int> RepeatOrNotList = new Dictionary<string, int>();
        public Dictionary<string, bool> EntryList = new Dictionary<string, bool>();
        public Dictionary<string, string> IsGreenOrRed = new Dictionary<string, string>();
        public Dictionary<string, string> EntrytxtList = new Dictionary<string, string>();
        //public Dictionary<string, bool> IsResetList = new Dictionary<string, bool>();
        public static int[] ansList;
        //public static TempDatabase AccDatabase;
        public static int num = 0;
        public static int ANS;
        public static string ANS2;
        public static string ANS3;
        public static string Qtype;
        public static List<string> tmpans_list = new List<string>();
        public static List<string> name_list = new List<string>();
        public static List<string> check_name_list = new List<string>();
        public static List<string> repeat_list = new List<string>();
        public static List<string> tmp_name_list = new List<string>();
        public static List<string> tmp_num_list = new List<string>();
        public static List<string> tmp_qb_list = new List<string>();
        public static List<string> qb03_list = new List<string>();
        public static List<string> entry_list = new List<string>();
        //public static List<string> postcheck_list = new List<string>();
        //public static List<string> qb02_list = new List<string>();

        public static string anslist;
        public static string p;
        public static TempDatabase AccDatabase;
        public static TempAddDatabase TempAddDB;
        public static TempAddDatabase_else TempAddDB_else;
        public static ChooseDatabase ChooseDB;
        public static ResetDatabase ResetDB;
        public static EntryDatabase EntryDB;
        public static EntrytxtDatabase EntrytxtDB;
        public static string _QB_S_NUM;
        public static string _sameans;
        public static string _sameans2;
        public static string _ans;
        public static int count = 0;
        public static int TFcount = 0;
        public static string result = "";
        public static int result_num;
        public static bool IsChoose = false;
        public static bool isRed = false;
        public static bool isGreen = false;
        public static bool isReset = false;
        public static bool isDB = false;
        public static string color;
        public static int qb03_count;
        public static string qb03;
        //public static string _wqh_s_num;
        //public static string temp_j = "";

        public TestView()
        {
            InitializeComponent();
            ChooseDB = new ChooseDatabase();
            ResetDB = new ResetDatabase();
            EntryDB = new EntryDatabase();
            EntrytxtDB = new EntrytxtDatabase();
            TempAddDB = new TempAddDatabase();
            TempAddDB_else = new TempAddDatabase_else();
            //////Console.WriteLine("way~~ " + MainPage.Loginway);
            if(MainPage.Loginway == "Auto")
            {
                TempAddDB.DeleteAll();
                TempAddDB_else.DeleteAll();
                //////Console.WriteLine("finally~~~ ");
            }
            
            Messager();
        }
        

        private async void setView()
        {
            //////Console.WriteLine("WAY~~~" + MainPage.Loginway);
            name_list.Clear();
            repeat_list.Clear();
            tmp_name_list.Clear();
            tmp_num_list.Clear();
            questionnaireslist = await web.Get_Questionaire(MainPage.token);
            //foreach (var value in questionnaireslist)
            //{
                //Console.WriteLine("value~~ " + value.ClientName);
                //Console.WriteLine("wqh~~ " + value.wqh_s_num);
                //Console.WriteLine("qh~~ " + value.qh_s_num);
                //Console.WriteLine("qb~~ " + value.qb_s_num);
                //Console.WriteLine("qb03~~ " + value.qbs.Count());
            //}

            DateTime today = DateTime.Now;
            string[] Day = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            //////Console.WriteLine("星期幾~~~ " + today.DayOfWeek);
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                result = "星期一";
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                result = "星期二";
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                result = "星期三";
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                result = "星期四";
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                result = "星期五";
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                result = "星期六";
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                result = "星期日";
            }
            //////Console.WriteLine("result~~~ " + result);
            for(int e = 0; e < Day.Count(); e++)
            {
                if(result == Day[e])
                {
                    result_num = e;
                }
            }
            ////Console.WriteLine("result_num~~~ " + result_num);
            ////Console.WriteLine("result~~ " + result);
            //Console.WriteLine("COUNT~~~" + questionnaireslist.Count());

            // 處理同上按鈕
            if(questionnaireslist.Count() != 0) // 防呆(防止後台沒傳資料近來)
            {
                for (int b = 0; b < questionnaireslist.Count(); b++) // 0是初始 1是正常 -1是重複
                {
                    if(questionnaireslist[b].ClientName != "")
                    {
                        RepeatOrNotList[questionnaireslist[b].ClientName] = 0;
                    }
                    else
                    {
                        await DisplayAlert("系統訊息", "後臺尚未產生資料或資料接收不齊全", "ok");
                        Console.WriteLine("AAA");
                    }
                }

                for (int a = 0; a < questionnaireslist.Count(); a++) // 0是初始 1是正常 -1是重複
                {
                    if (questionnaireslist[a].ClientName != "")
                    {
                        check_name_list.Add(questionnaireslist[a].ClientName);
                        if (RepeatOrNotList.ContainsKey(questionnaireslist[a].ClientName) == true && RepeatOrNotList[questionnaireslist[a].ClientName] == 1)
                        {
                            RepeatOrNotList[questionnaireslist[a].ClientName] = -1;
                            //////Console.WriteLine("Addrepeat~~ ");
                            //////Console.WriteLine("Add~~~ " + questionnaireslist[a].ClientName);

                        }
                        else
                        {
                            RepeatOrNotList[questionnaireslist[a].ClientName] = 1;
                        }
                    }
                    else
                    {
                        await DisplayAlert("系統訊息", "後臺尚未產生資料或資料接收不齊全", "ok");
                    }

                }
                foreach (var value in questionnaireslist)
                {
                    //Console.WriteLine("value.qb_s_num~~ " + value.qb_s_num);
                    if(value.ClientName != "")
                    {
                        CheckboxList[value.ClientName] = false;
                        //Console.WriteLine("value~~ " + value.ClientName);
                        //Console.WriteLine("wqh~~ " + value.wqh_s_num);

                        //Console.WriteLine("count~~ " + value.qbs.Count());
                        for (int i = 0; i < value.qbs.Count; i++)
                        {
                            //Console.WriteLine("qb03~~ " + value.qbs[i].qb03);
                        }
                        //Console.WriteLine("qb03~~ " + value.qbs[0].qb03);
                        //Console.WriteLine("valuename~~ " + value.ClientName);
                        questionView(value);






                        //////Console.WriteLine("value~~~" + value.ClientName);
                        uploadbtn.IsVisible = true;
                        uploadbtn.IsEnabled = true;
                    }
                    else
                    {
                        await DisplayAlert("系統訊息", "後臺尚未產生資料或資料接收不齊全", "ok");
                    }

                }
            }
            
           
        }

        private void reset()
        {
            ////////Console.WriteLine("RRRR~~~");
            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
            ////////Console.WriteLine("chooseDB~~ " + ChooseDB.GetAccountAsync2().Count());
            ////////Console.WriteLine("resetDB~~ " + ResetDB.GetAccountAsync2().Count());
            ////////Console.WriteLine("entryDB~~ " + EntryDB.GetAccountAsync2().Count());
            TFcount = 0;
            quesStack.Children.Clear();
            foreach (var value in questionnaireslist)
            {
                questionView(value);
                //////Console.WriteLine("wqh_reset~~~ " + value.wqh_s_num);
            }
           
            
        }

        

        

        public StackLayout questionView(questionnaire questionList)
        {
            //StackLayout stack2 = new StackLayout // stacklayout看裡面包什麼寫在children
            //{
            //    Children = { }
            //};
            ////////Console.WriteLine("name~ " + questionList.ClientName);
            ////////Console.WriteLine("qb_s_num~~ " + questionList.qbs[0].qb_s_num);
            
            
            var label_name = new Label
            {
                Text = questionList.ClientName,
                TextColor = Color.DarkBlue,
                FontSize = 20
            };

            var label_wqh = new Label
            {
                Text = questionList.wqh_s_num
            };
            var label_qh = new Label
            {
                Text = questionList.qh_s_num
            };

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(5, 0, 5, 0),
                // BackgroundColor = Color.FromHex("eddcd2"),
                Children = { label_name, label_wqh, label_qh } // 標題(人名 + 問卷編號 +)
            };

            // TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(num).FirstOrDefault();
            //for (int a = 0; a < questionList.qbs.Count(); a++)
            //{
            //    string set = questionList.wqh_s_num + questionList.qbs[a];
            //    TmpCheckList[set] = false;
            //}


            
            foreach (var i in questionList.qbs) // 看問題有幾個就跑幾次
            {
                //////Console.WriteLine("qqq~~ " + i.qb03[0]);
                //////Console.WriteLine("qb03~~~" + i.qb03.Count());
                //Console.WriteLine("qborderIN~~~ " + i.qb_order);
                qb03_list.Clear();
                TFcount = 0;
                _QB_S_NUM = i.qb_s_num;
                //Console.WriteLine("_QB_S_NUM@@@@ " + _QB_S_NUM);
                //Console.WriteLine("qbssss~~ " + questionList.qb_s_num);
                TmpAddList[questionList.wqh_s_num + i.qb_order] = false;
                TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = false;
                IsResetList[questionList.wqh_s_num + i.qb_order] = false;
                EntrytxtList[questionList.wqh_s_num + i.qb_order] = "";
                IsGreenOrRed[questionList.wqh_s_num + i.qb_order] = "";
                EntryList[questionList.ClientName] = false;
                TmpAnsList[questionList.ClientName + i.qb_order] = "";
                qb03_list.Clear();
               
                //////Console.WriteLine("ques~~~" + questionList.qb_s_num);
                //////Console.WriteLine("qhsnum~~~" + questionList.qh_s_num);
                //////Console.WriteLine("qbsnum~~"  + questionList.qbs[0].qb_s_num);
                //////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                //IsResetList[questionList.wqh_s_num + i.qb_order] = false;
                //////Console.WriteLine("chooseDB~~ " + ChooseDB.GetAccountAsync2().Count());
                if (ChooseDB.GetAccountAsync2().Count() > 0) // database裡面有資料
                {
                    ////////Console.WriteLine("IMMMM~~~~");
                    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                    for (int b = 0; b < ChooseDB.GetAccountAsync2().Count(); b++)
                    {
                        var a = ChooseDB.GetAccountAsync(b);


                        foreach (var TempChooseList in a)
                        {
                            if(TempChooseList.ClientName == questionList.ClientName)
                            {
                                //////Console.WriteLine("choose_wqh~~ " + questionList.wqh_s_num);
                                CheckboxList[questionList.ClientName] = true;
                            }
                            
                        }
                    }
                }
                ////////Console.WriteLine("ResetDB~~ " + ResetDB.GetAccountAsync2().Count());
                if(ResetDB.GetAccountAsync2().Count() > 0)
                {
                    for(int c = 0; c < ResetDB.GetAccountAsync2().Count(); c++)
                    {
                        var d = ResetDB.GetAccountAsync(c);
                        foreach(var TempResetList in d)
                        {
                            if (TempResetList.wqh_s_num == questionList.wqh_s_num)
                            {
                                if (TempResetList.qb_order == i.qb_order)
                                {
                                    IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                    isDB = true;
                                    if(TempResetList.color == "Red")
                                    {
                                        //isRed = true;
                                        //isGreen = false;
                                        IsGreenOrRed[questionList.wqh_s_num + i.qb_order] = "Red";
                                    }
                                    else
                                    {
                                        //isGreen = true;
                                        //isRed = false;
                                        IsGreenOrRed[questionList.wqh_s_num + i.qb_order] = "Green";
                                    }
                                    //Console.WriteLine("bingoname~~~ " + TempResetList.wqh_s_num);
                                    //Console.WriteLine("qborder~~ " + i.qb_order);
                                    //Console.WriteLine("color~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                }
                            }
                        }
                    }
                }
                ////////Console.WriteLine("EntryDB~~ " + EntryDB.GetAccountAsync2().Count());
                if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                {
                    ////////Console.WriteLine("1111~~~~");
                    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                    {
                        var a = MapView.AccDatabase.GetAccountAsync(b);

                        //////Console.WriteLine("2222~~~~");
                        foreach (var TempAnsList in a)
                        {
                            ////////Console.WriteLine("3333~~~~");
                            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                            //////////Console.WriteLine("WHO~~" + who);
                            //////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                            ////////Console.WriteLine("temp000~~~ " + TempAnsList.wqh_s_num);
                            ////////Console.WriteLine("ques~~~ " + questionList.wqh_s_num);
                            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            {
                                ////////Console.WriteLine("4444~~~~");
                                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                {
                                    ////////Console.WriteLine("ans~~ " + TempAnsList.wqb01);
                                    ////////Console.WriteLine("wqh~~ " + TempAnsList.wqh_s_num);
                                    ////////Console.WriteLine("name~~~ " + TempAnsList.ClientName);
                                    TmpAnsList[questionList.ClientName + i.qb_order] = TempAnsList.wqb01;
                                    ////////Console.WriteLine("EEE~~~ " + TmpAnsList[questionList.ClientName + i.qb_order]);
                                    //////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //////////Console.WriteLine("who " + TempAnsList.qh_s_num);

                                    for (int c = 0; c < checkList2.Count(); c++)
                                    {
                                        if (checkList2[c].wqh_s_num == questionList.wqh_s_num)
                                        {
                                            if (checkList2[c].qb_s_num == i.qb_s_num)
                                            {
                                                //checkList.RemoveAt(a);
                                                checkList2.RemoveAt(c);
                                            }
                                        }


                                    }

                                }
                            }


                        }
                    }



                }
                ////////Console.WriteLine("TempAdd~~~ " + TempAddDB.GetAccountAsync2().Count());
                if (TempAddDB.GetAccountAsync2().Count() > 0) // database裡面有資料
                {
                    //////////Console.WriteLine("IMMMM~~~~");
                    //////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                    for (int b = 0; b < TempAddDB.GetAccountAsync2().Count(); b++)
                    {
                        var a = TempAddDB.GetAccountAsync(b);


                        foreach (var AddList in a)
                        {
                            if (AddList.wqh_s_num == questionList.wqh_s_num)
                            {
                                if(AddList.qb_order == i.qb_order)
                                {
                                    TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                    ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]);
                                }
                                ////////Console.WriteLine("choose_wqh~~ " + questionList.wqh_s_num);
                                
                            }

                        }
                    }
                }
                if (TempAddDB_else.GetAccountAsync2().Count() > 0) // database裡面有資料
                {
                    //////////Console.WriteLine("IMMMM~~~~");
                    //////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                    for (int b = 0; b < TempAddDB_else.GetAccountAsync2().Count(); b++)
                    {
                        var a = TempAddDB_else.GetAccountAsync(b);


                        foreach (var Add_elseList in a)
                        {
                            if (Add_elseList.wqh_s_num == questionList.wqh_s_num)
                            {
                                if (Add_elseList.qb_order == i.qb_order)
                                {
                                    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                                    ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                                }
                                ////////Console.WriteLine("choose_wqh~~ " + questionList.wqh_s_num);

                            }

                        }
                    }
                }
                //////////Console.WriteLine("qbsnum1~~" + questionList.qbs[1].qb_s_num);
                //////////Console.WriteLine("qbsnum2~~" + questionList.qbs[2].qb_s_num);
                //////////Console.WriteLine("qbsnum3~~" + questionList.qbs[3].qb_s_num);
                //////////Console.WriteLine("QBS~~" + questionList.qbs.Count());

                // 單選題(有entry)
                //if (i.qb02 == "1") // 問題類型(假設1是是否題 / 單選)(有entry版本)
                //{
                //    Qtype = "1";
                //    //string set = questionList.wqh_s_num + i.qb_s_num;
                //    //TmpCheckList[set] = false;
                //    var label_que_name = new Label // 問題題號+題目
                //    {
                //        Text = i.qb_order + " " + i.qb01,
                //        FontSize = 20,
                //        TextColor = Color.Black
                //    };

                //    var stack_ques = new StackLayout
                //    {
                //        Orientation =  StackOrientation.Horizontal
                //    };

                //    foreach(var j in i.qb03s) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                //    {
                //        //count = 0;
                //        var temp_j = "";
                //        var temp_value = "";
                //        //var temp_j_map = "";
                //        //var temp_value_map = "";

                //        // 跑選是的reset把checkList抓回來判斷
                //        for (int a = 0; a < checkList.Count(); a++)
                //        {
                //            //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                //            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                //            {
                //                ////////Console.WriteLine("IMMMM222~~~~");
                //                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                //                {
                //                    temp_j = checkList[a].wqb01; // 答案
                //                    temp_value = checkList[a].wqb99; // entry
                //                }
                //            }
                //        }

                //        if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                //        {
                //            ////////Console.WriteLine("IMMMM~~~~");
                //            ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                //            for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                //            {
                //                var a = MapView.AccDatabase.GetAccountAsync(b);


                //                foreach (var TempAnsList in a)
                //                {
                //                    string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                //                    ////////Console.WriteLine("WHO~~" + who);
                //                    ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                //                    if (TmpCheckList[who] == false)
                //                    {
                //                        if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                //                        {
                //                            if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                //                            {
                //                                ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                //                                ////////Console.WriteLine("who " + TempAnsList.qh_s_num);

                //                                TmpCheckList[who] = true;
                //                                temp_j = TempAnsList.wqb01;
                //                                temp_value = TempAnsList.wqb99;
                //                                var check2 = new checkInfo
                //                                {
                //                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                //                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                //                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                //                                    wqb01 = TempAnsList.wqb01,// 答案
                //                                    wqb99 = TempAnsList.wqb99

                //                                };
                //                                ////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                //                                ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                //                                ////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                //                                ////////Console.WriteLine("ID" + TempAnsList.ID);
                //                                checkList.Add(check2);
                //                                //MapView.AccDatabase.DeleteItem(b);
                //                            }
                //                        }
                //                    }
                //                    //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                //                    //var check2 = new checkInfo
                //                    //{
                //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                //                    //    wqb01 = TempAnsList.wqb01,// 答案
                //                    //    wqb99 = TempAnsList.wqb99

                //                    //};
                //                    //checkList.Add(check2);

                //                }
                //            }



                //        }



                //        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                //        bool isEntry = (temp_j == "是") ? true : false; // 如果答案是 是 -> entry顯示


                //        var check_box = new CheckBox // 產生checkbox
                //        {
                //            IsChecked = ischeck,
                //            Color = Color.FromHex("264653")
                //        };
                //        if (j == "是")
                //        {
                //            entny = new Entry // 產生Entry
                //            {
                //                Placeholder = "請說明",
                //                Text = temp_value,
                //                IsVisible = isEntry,
                //                IsEnabled = isEntry


                //            };



                //            entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                //            {
                //                for (int a = 0; a < checkList.Count(); a++)
                //                {
                //                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                //                    {
                //                        if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                //                        {
                //                            ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                //                            ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                //                            checkList[a].wqb99 = ee.NewTextValue;
                //                            ////////Console.WriteLine("Text~" + ee.NewTextValue);
                //                        }

                //                    }

                //                }
                //                //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                //                //{
                //                //    ////////Console.WriteLine("IMMMM~~~~");
                //                //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                //                //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                //                //    {
                //                //        var c = MapView.AccDatabase.GetAccountAsync(b);


                //                //        foreach (var TempAnsList in c)
                //                //        {
                //                //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                //                //            ////////Console.WriteLine("WHO~~" + who);
                //                //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                //                //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                //                //            {
                //                //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                //                //                {
                //                //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                //                //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                //                //                    for (int d = 0; d < checkList.Count(); d++)
                //                //                    {
                //                //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                //                //                        {
                //                //                            checkList[d].wqb99 = TempAnsList.wqb99;
                //                //                        }

                //                //                    }

                //                //                    //temp_j = TempAnsList.wqb01;
                //                //                    //temp_value = TempAnsList.wqb99;
                //                //                    //var check2 = new checkInfo
                //                //                    //{
                //                //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                //                //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                //                //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                //                //                    //    wqb01 = TempAnsList.wqb01,// 答案
                //                //                    //    wqb99 = TempAnsList.wqb99

                //                //                    //};

                //                //                    //checkList.Add(check2);

                //                //                    //MapView.AccDatabase.DeleteItem(b);
                //                //                }
                //                //            }


                //                //        }
                //                //    }



                //                //}
                //                //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                //                //{
                //                //    ////////Console.WriteLine("IMMMM~~~~");

                //                //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                //                //    {
                //                //        var a = MapView.AccDatabase.GetAccountAsync(b);
                //                //        foreach (var TempAnsList in a)
                //                //        {
                //                //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                //                //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                //                //            {
                //                //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                //                //                {
                //                //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                //                //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                //                //                    //temp_j = TempAnsList.wqb01;
                //                //                    for (int c = 0; c < checkList.Count(); c++)
                //                //                    {
                //                //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                //                //                        {
                //                //                            checkList[c].wqb99 = TempAnsList.wqb99;
                //                //                        }
                //                //                    }
                //                //                }
                //                //            }

                //                //        }
                //                //    }


                //                //}
                //            };
                //        }
                //        check_box.CheckedChanged += async (s, e) =>
                //        {
                //            if(e.Value) // 如果選是，要跳出entry所以需要reset
                //            {
                //                ////////Console.WriteLine("IN~~~");
                //                if(j == "是")
                //                {
                //                    ANS = 0;
                //                    ANS2 = Convert.ToString(ANS);
                //                }
                //                else
                //                {
                //                    ANS = 1;
                //                    ANS2 = Convert.ToString(ANS);
                //                }
                //                var check = new checkInfo
                //                {
                //                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                //                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                //                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                //                    wqb01 = j // 答案

                //                };
                //                var check3 = new checkInfo
                //                {
                //                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                //                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                //                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                //                    qb_order = i.qb_order
                //                    wqb01 = ANS2 // 答案

                //                };
                //                checkList.Add(check); // for check
                //                checkList2.Add(check3); // for save
                //                //////////Console.WriteLine("CHECK" + checkList[0]);
                //                reset();
                //                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                //            } else // 選否則不用reset
                //            {
                //                for(int a=0; a < checkList.Count(); a++)
                //                {
                //                    if(checkList[a].qb_s_num == i.qb_s_num)
                //                    {
                //                        checkList.RemoveAt(a);
                //                    }
                //                }
                //            }

                //            foreach(var b in checkList)
                //            {
                //                ////////Console.WriteLine("HERE~~");
                //                ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                //                ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                //                ////////Console.WriteLine("qb03 : " + b.wqb01);
                //                ////////Console.WriteLine("enrty : " + b.wqb99);
                //            }
                //        };

                //        var label_check = new Label // 選項
                //        {
                //            Text = j,
                //            TextColor = Color.Black,
                //            FontSize = 20
                //        };

                //        var stack_check = new StackLayout // checkbox跟選項
                //        {
                //            Orientation = StackOrientation.Horizontal,
                //            Children = { check_box, label_check}
                //        };

                //        //var ques_all_check = new StackLayout
                //        //{
                //        //    Orientation = StackOrientation.Horizontal,
                //        //    Children = { stack_check, stack }
                //        //};

                //        var stack_enrty = new StackLayout
                //        {
                //            Orientation = StackOrientation.Vertical,
                //            Children = { stack_check, entny}
                //        };
                //        stack_ques.Children.Add(stack_enrty);
                //        //count = count + 1;

                //        //var final_stack = new StackLayout
                //        //{
                //        //    Orientation = StackOrientation.Horizontal,
                //        //    Children = { stack_ques, label_que_name }
                //        //};
                //    }

                //    quesStack.Children.Add(stack); // w

                //    //quesStack.Children.Add(final_stack);
                //    //quesStack.Children.Add(label_que_name);
                //    //quesStack.Children.Add(stack_ques);
                //    var final_stack = new StackLayout
                //    {
                //        Orientation = StackOrientation.Horizontal,
                //        Children = {label_que_name, stack_ques }
                //    };


                //    Frame frame = new Frame // frame包上面那個stacklayout
                //    {
                //        Padding = new Thickness(10, 5, 10, 5),
                //        BackgroundColor = Color.FromHex("eddcd2"),
                //        CornerRadius = 10,
                //        HasShadow = false,
                //        Content = final_stack
                //    };
                //    quesStack.Children.Add(frame);

                //    //var last_stack = new StackLayout
                //    //{
                //    //    Orientation = StackOrientation.Vertical,
                //    //    Children = { stack, frame }
                //    //};

                //    //ScrollView scrollView = new ScrollView { Content = quesStack };
                //    //Content = scrollView;
                //    //ScrollView scrollView = new ScrollView
                //    //{
                //    //    Content = quesStack

                //    //};
                //    //quesStack.Children.Add(scrollView);
                //    //ScrollView scrollView = new ScrollView { Content = quesStack };
                //}
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //////////Console.WriteLine("ischoose~~~ " + IsChoose);
                ////////Console.WriteLine("order~~~ " + i.qb_order);
                ////////Console.WriteLine("AAA~~~ " + CheckboxList[questionList.ClientName]);
                //Console.WriteLine("qb_order~~~ " + i.qb_order);
                //Console.WriteLine("qborderint~~~ " + Int32.Parse(i.qb_order));
                ////////Console.WriteLine("name~~ " + questionList.ClientName);
                ////////Console.WriteLine("out~~ " + questionList.wqh_s_num);
                //Console.WriteLine("name~~~ " + questionList.ClientName);
                //Console.WriteLine("CheckboxList~~~ " + CheckboxList[questionList.ClientName + questionList.qb_s_num]);
                if(Int32.Parse(i.qb_order) < 4 && CheckboxList[questionList.ClientName] == false)
                {
                    ////////Console.WriteLine("inA~~~~ ");
                    if (i.qb02 == "1") // 問題類型(假設1是是否題 / 單選)(沒有entry版本)
                    {
                        Qtype = "1";
                        //string set = questionList.wqh_s_num + i.qb_s_num;
                        //TmpCheckList[set] = false;
                        var label_que_name = new Label // 問題題號+題目
                        {
                            Text = i.qb_order + " " + i.qb01,
                            FontSize = 20,
                            TextColor = Color.Black
                        };

                        var stack_ques = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal
                        };
                        ////////Console.WriteLine("i.qb03~~~ " + i.qb03);
                        ////////Console.WriteLine("i.qb_order~~~ " + i.qb_order);
                        ////////Console.WriteLine("result~~~ " + result);
                        ////////Console.WriteLine("TorF~~~" + result == "星期三");
                        ////////Console.WriteLine("TorF2~~~~ " + result.Equals("星期三"));
                       
                        ////////Console.WriteLine("value~~ " + questionList.wqh_s_num);
                        ////////Console.WriteLine("checklist2~count0~ " + checkList2.Count());
                        if(i.qb_order == "3" && questionList.qbs.Count() == 5 && result_num < 3)
                        {
                            ////Console.WriteLine("nameBB~~~ " + questionList.ClientName);
                            ////Console.WriteLine("countBB~~~ " + questionList.qbs.Count());
                            ////Console.WriteLine("order~~~ " + i.qb_order);
                            ////Console.WriteLine("result_num~~~ " + result_num);
                            ////////Console.WriteLine("reseultnum~~LA~~~ " + result_num);
                            ////////Console.WriteLine("LKJ~~~~~~ ");
                            string[] list = { "已發" };
                            foreach (var j in list) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            {
                                //////Console.WriteLine("LKJH~~~ " + j);
                                //count = 0;
                                TFcount = TFcount + 1;
                                var temp_j = "";
                                var temp_value = "";
                                //var temp_j_map = "";
                                //var temp_value_map = "";

                                // 跑選是的reset把checkList抓回來判斷
                                if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                {
                                    ////////Console.WriteLine("first~~ ");
                                    ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    var _wqhsnum = questionList.wqh_s_num;
                                    temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                    ////////Console.WriteLine("tempj~~ " + temp_j);
                                    for (int d = 0; d < i.qb03.Count(); d++)
                                    {
                                        ////////Console.WriteLine("j00~~ " + j);
                                        ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                        if (temp_j == i.qb03[d])
                                        {

                                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                            //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                            ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            ////////Console.WriteLine("j~~ " + j);
                                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                            //ANS2 = Convert.ToString(qb03_count);
                                            ANS2 = d.ToString();
                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                            ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                        }

                                        ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                    }
                                    ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]);
                                    if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                    {
                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = ANS2 // 答案

                                        };
                                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        checkList2.Add(check3); // for save
                                        AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                        TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                        ////////Console.WriteLine("checkList2Add2~~~ ");
                                    }

                                    // ////////Console.WriteLine("cc~~~ " + p);
                                    ////////Console.WriteLine("ANS2~~ " + ANS2);
                                    //var ques = questionList.wqh_s_num + i.qb_order;
                                    //////////Console.WriteLine("ques!!! " + ques);
                                    //for(int e = 0; e < checkList2.Count(); e++)
                                    //{
                                    //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                    //    ////////Console.WriteLine("check!!! " + check);
                                    //    if (check != ques)
                                    //    {
                                    //        ////////Console.WriteLine("checkin!!! " + check);
                                    //        ////////Console.WriteLine("quesin!!! " + ques);
                                    //        var check3 = new checkInfo
                                    //        {
                                    //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //            qb_order = i.qb_order,
                                    //            wqb01 = ANS2 // 答案

                                    //        };
                                    //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                    //        checkList2.Add(check3); // for save
                                    //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                    //    }
                                    //}
                                    ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                }
                                // 跑選是的reset把checkList抓回來判斷
                                ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                for (int a = 0; a < checkList.Count(); a++)
                                {
                                    ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                    ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    {
                                        ////////Console.WriteLine("IMMMM222~~~~");
                                        //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                        if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                        {
                                            //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                            //foreach (var w in i.qb03)
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                //////Console.WriteLine("w00~~~ " + d.ToString());
                                                if (checkList[a].wqb01 == d.ToString())
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    temp_j = i.qb03[d]; // 答案
                                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);

                                            //temp_value = checkList[a].wqb99; // entry
                                        }
                                    }
                                }



                                bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                             //IsChoose = (temp_j == "未發") ? true : false; // 如果答案是 是 -> entry顯示
                                if (ischeck == true)
                                {
                                    if (j == "是" || j == "已發")
                                    {
                                        isRed = true;
                                    }
                                    else
                                    {
                                        isGreen = true;
                                    }
                                }
                                //////Console.WriteLine("j2~~~ " + j);
                                //////Console.WriteLine("isckeck2~~~~ " + ischeck);
                                //////Console.WriteLine("isRed2~~~ " + isRed);
                                //////Console.WriteLine("isGreen2~~~ " + isGreen);


                                //////Console.WriteLine("TFcount~~~" + TFcount);
                                if (TFcount == 1)
                                {
                                    check_box = new CheckBox // 產生checkbox
                                    {

                                        IsChecked = ischeck,
                                        Margin = new Thickness(-5, 0, 0, 0),
                                        //Color = Color.FromHex("264653")
                                        Color = Color.Red
                                    };
                                }
                                else
                                {
                                    check_box = new CheckBox // 產生checkbox
                                    {

                                        IsChecked = ischeck,
                                        Margin = new Thickness(-5, 0, 0, 0),
                                        //Color = Color.FromHex("264653")
                                        Color = Color.Green
                                    };
                                }



                                //if (j == "是")
                                //{
                                //entny = new Entry // 產生Entry
                                //{
                                //    Placeholder = "請說明",
                                //    Text = temp_value,
                                //    IsVisible = isEntry,
                                //    IsEnabled = isEntry


                                //};



                                //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                //{
                                //    for (int a = 0; a < checkList.Count(); a++)
                                //    {
                                //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                //        {
                                //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                //            {
                                //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                //                checkList[a].wqb99 = ee.NewTextValue;
                                //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                //            }

                                //        }

                                //    }
                                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                //    //{
                                //    //    //////Console.WriteLine("IMMMM~~~~");
                                //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                //    //    {
                                //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                //    //        foreach (var TempAnsList in c)
                                //    //        {
                                //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                //    //            //////Console.WriteLine("WHO~~" + who);
                                //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                //    //            {
                                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                //    //                {
                                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                //    //                    for (int d = 0; d < checkList.Count(); d++)
                                //    //                    {
                                //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                //    //                        {
                                //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                //    //                        }

                                //    //                    }

                                //    //                    //temp_j = TempAnsList.wqb01;
                                //    //                    //temp_value = TempAnsList.wqb99;
                                //    //                    //var check2 = new checkInfo
                                //    //                    //{
                                //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                //    //                    //    wqb99 = TempAnsList.wqb99

                                //    //                    //};

                                //    //                    //checkList.Add(check2);

                                //    //                    //MapView.AccDatabase.DeleteItem(b);
                                //    //                }
                                //    //            }


                                //    //        }
                                //    //    }



                                //    //}
                                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                //    //{
                                //    //    //////Console.WriteLine("IMMMM~~~~");

                                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                //    //    {
                                //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                //    //        foreach (var TempAnsList in a)
                                //    //        {
                                //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                //    //            {
                                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                //    //                {
                                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                //    //                    //temp_j = TempAnsList.wqb01;
                                //    //                    for (int c = 0; c < checkList.Count(); c++)
                                //    //                    {
                                //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                //    //                        {
                                //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                //    //                        }
                                //    //                    }
                                //    //                }
                                //    //            }

                                //    //        }
                                //    //    }


                                //    //}
                                //};
                                //}
                                check_box.CheckedChanged += async (s, e) =>
                                {
                                    //////Console.WriteLine("checkboxin2~~~");
                                    if (e.Value) // 如果選是，要跳出entry所以需要reset
                                    {
                                        ////////Console.WriteLine("IN~~~");
                                        //ischeck = true;
                                        ////////Console.WriteLine("evalue~~~ " + e.Value.ToString());
                                        //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                    //checkList2.RemoveAt(a);
                                                }
                                            }

                                        }
                                        ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                        //if (tmp_name_list.Contains(questionList.ClientName))
                                        //{
                                        //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                        //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                        //    //////Console.WriteLine("a~ " + total);
                                        //    tmp_name_list.Remove(questionList.ClientName);
                                        //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                        //    //////Console.WriteLine("b~ " + total2);
                                        //}

                                        //if (j == "是")
                                        //{
                                        //    ANS = 0;
                                        //    ANS2 = Convert.ToString(ANS);
                                        //}
                                        //else
                                        //{
                                        //    ANS = 1;
                                        //    ANS2 = Convert.ToString(ANS);
                                        //}
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            //////Console.WriteLine("j00~~ " + j);
                                            //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (j == i.qb03[d])
                                            {

                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                //////Console.WriteLine("j~~ " + j);
                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            //////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        // //////Console.WriteLine("cc~~~ " + p);
                                        //////Console.WriteLine("ANS2~~ " + ANS2);
                                        // 把問題選項存進資料庫
                                        ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                        //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                        //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                        ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                        ////////Console.WriteLine("j~~" + j);
                                        if (j == "是" || j == "已發")
                                        {
                                            color = "Red";
                                        }
                                        else
                                        {
                                            color = "Green";
                                        }
                                        //////Console.WriteLine("color~~~ " + color);
                                        QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                        ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                        var check = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            wqb01 = ANS2 // 答案

                                        };
                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = ANS2 // 答案

                                        };
                                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        checkList2.Add(check3); // for save
                                                                //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                        checkList.Add(check); // for check
                                                              //checkList2.Add(check3); // for save
                                                              ////////Console.WriteLine("CHECK" + checkList[0]);
                                        isReset = true;
                                        reset();

                                        // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                    }
                                    else // 選否則不用reset
                                    {
                                        //ischeck = false;
                                        //////Console.WriteLine("remove~~~~~");
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            if (checkList[a].qb_s_num == i.qb_s_num)
                                            {
                                                checkList.RemoveAt(a);
                                            }
                                        }
                                    }

                                    //foreach (var b in checkList)
                                    //{
                                    //    //////Console.WriteLine("HERE~~");
                                    //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                    //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                    //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                    //    //////Console.WriteLine("enrty : " + b.wqb99);
                                    //}
                                };

                                if (isReset == true || isDB == true)
                                {
                                    if (isRed == true && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                    {
                                        //////Console.WriteLine("JKL1~~~ " + j);
                                        if (j == "是" || j == "已發")
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Red,
                                                FontSize = 20
                                            };
                                            isRed = false;
                                        }

                                    }
                                    else if (isGreen == true && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                    {
                                        //////Console.WriteLine("JKL2~~~ " + j);
                                        if (j == "未發" || j == "否")
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Green,
                                                FontSize = 20
                                            };
                                            isGreen = false;
                                        }

                                    }
                                    else
                                    {
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }
                                }
                                else
                                {
                                    label_check = new Label // 選項
                                    {
                                        Text = j,
                                        TextColor = Color.Black,
                                        FontSize = 20
                                    };
                                }

                                var stack_check = new StackLayout // checkbox跟選項
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children = { check_box, label_check }
                                };

                                //var ques_all_check = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Horizontal,
                                //    Children = { stack_check, stack }
                                //};

                                //var stack_enrty = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Vertical,
                                //    Children = { stack_check, entny }
                                //};
                                stack_ques.Children.Add(stack_check);
                                //count = count + 1;

                                //var final_stack = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Horizontal,
                                //    Children = { stack_ques, label_que_name }
                                //};
                            }
                        } // 星期 1 ~ 3 只有已發
                        else
                        {
                            foreach (var j in i.qb03)
                            {

                                if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期五") == true)
                                {
                                    ////////Console.WriteLine("friday_in~~~ ");
                                    //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                    //{
                                    //count = 0;
                                    ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                                    ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                                    ////Console.WriteLine("order~~~ " + i.qb_order);
                                    ////Console.WriteLine("result_num~~~ " + result_num);
                                    TFcount = TFcount + 1;
                                    var temp_j = "";
                                    var temp_value = "";
                                    //var temp_j_map = "";
                                    //var temp_value_map = "";
                                    ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                                    if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                    {
                                        ////////Console.WriteLine("first~~ ");
                                        ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        var _wqhsnum = questionList.wqh_s_num;
                                        temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                        ////////Console.WriteLine("tempj~~ " + temp_j);
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            ////////Console.WriteLine("j00~~ " + j);
                                            ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (temp_j == i.qb03[d])
                                            {

                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                ////////Console.WriteLine("j~~ " + j);
                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                ////////Console.WriteLine("jj~~ " + temp_j);
                                                ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                                        //if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                        //{
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                            AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                            ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //}

                                        // ////////Console.WriteLine("cc~~~ " + p);
                                        ////////Console.WriteLine("ANS2~~ " + ANS2);
                                        //var ques = questionList.wqh_s_num + i.qb_order;
                                        //////////Console.WriteLine("ques!!! " + ques);
                                        //for(int e = 0; e < checkList2.Count(); e++)
                                        //{
                                        //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                        //    ////////Console.WriteLine("check!!! " + check);
                                        //    if (check != ques)
                                        //    {
                                        //        ////////Console.WriteLine("checkin!!! " + check);
                                        //        ////////Console.WriteLine("quesin!!! " + ques);
                                        //        var check3 = new checkInfo
                                        //        {
                                        //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //            qb_order = i.qb_order,
                                        //            wqb01 = ANS2 // 答案

                                        //        };
                                        //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        //        checkList2.Add(check3); // for save
                                        //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //    }
                                        //}
                                        ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                        //qb03_count = 0;
                                        //foreach (var y in i.qb03)
                                        //{
                                        //    ////////Console.WriteLine("yAA~~~ " + y);
                                        //    if (temp_j == y)
                                        //    {
                                        //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                        //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                        //        p = Convert.ToString(qb03_count);
                                        //    }
                                        //    qb03_count = qb03_count + 1;

                                        //}
                                        //////////Console.WriteLine("cc~~~ " + p);
                                        ////temp_value = TempAnsList.wqb99; // 之前的entry
                                        //var check2 = new checkInfo
                                        //{
                                        //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    wqb01 = p,// 答案
                                        //              //wqb99 = TempAnsList.wqb99

                                        //};
                                    }
                                    // 跑選是的reset把checkList抓回來判斷
                                    ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                    for (int a = 0; a < checkList.Count(); a++)
                                    {
                                        ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                        ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                        //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        {
                                            //////////Console.WriteLine("IMMMM222~~~~");
                                            ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                            {
                                                ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                //foreach (var w in i.qb03)
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                    ////////Console.WriteLine("w00~~~ " + d.ToString());
                                                    if (checkList[a].wqb01 == d.ToString())
                                                    {

                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        ////////Console.WriteLine("j~~ " + j);
                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        temp_j = i.qb03[d]; // 答案
                                                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                                    }

                                                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // ////////Console.WriteLine("cc~~~ " + p);
                                                ////////Console.WriteLine("ANS2~~ " + ANS2);

                                                //temp_value = checkList[a].wqb99; // entry
                                            }
                                        }
                                    }
                                    ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                                    ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                                    ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                                    // ------------------------------------------

                                    //////////Console.WriteLine("")
                                    //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //~~~~~~~~~~~~~~~~~~~~~~~~
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                    ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                                    ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                                    ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                                    ////////////Console.WriteLine("ID" + TempAnsList.ID);
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    //checkList.Add(check2);
                                    //checkList2.Add(check2);
                                    //~~~~~~~~~~~~~~~~~~~~`
                                    ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                                    ////////Console.WriteLine("j~~ " + j);

                                    bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                 //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                    if (ischeck == true)
                                    {
                                        //if (j == "是" || j == "已發")
                                        //{
                                        //    isRed = true;
                                        //}
                                        //else
                                        //{
                                        //    isGreen = true;
                                        //}

                                    }
                                    ////////Console.WriteLine("j1~~~ " + j);
                                    ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                                    ////////Console.WriteLine("isRed1~~~ " + isRed);
                                    ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                                    ////////Console.WriteLine("TFcount~~~" + TFcount);
                                    //if (j == "是" || j == "已發")
                                    ////////Console.WriteLine("name~~ " + questionList.ClientName);
                                    ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (TFcount == 1)
                                    {
                                        ////////Console.WriteLine("Red~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Green
                                        };
                                    }
                                    else
                                    {
                                        ////////Console.WriteLine("Green~~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Red
                                        };
                                    }



                                    //if (j == "是")
                                    //{
                                    //entny = new Entry // 產生Entry
                                    //{
                                    //    Placeholder = "請說明",
                                    //    Text = temp_value,
                                    //    IsVisible = isEntry,
                                    //    IsEnabled = isEntry


                                    //};



                                    //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                    //{
                                    //    for (int a = 0; a < checkList.Count(); a++)
                                    //    {
                                    //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                    //        {
                                    //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                    //            {
                                    //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                    //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                    //                checkList[a].wqb99 = ee.NewTextValue;
                                    //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                                    //            }

                                    //        }

                                    //    }
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");
                                    //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                    //    //        foreach (var TempAnsList in c)
                                    //    //        {
                                    //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                    //    //            ////////Console.WriteLine("WHO~~" + who);
                                    //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    for (int d = 0; d < checkList.Count(); d++)
                                    //    //                    {
                                    //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                    //    //                        }

                                    //    //                    }

                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    //temp_value = TempAnsList.wqb99;
                                    //    //                    //var check2 = new checkInfo
                                    //    //                    //{
                                    //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                    //    //                    //    wqb99 = TempAnsList.wqb99

                                    //    //                    //};

                                    //    //                    //checkList.Add(check2);

                                    //    //                    //MapView.AccDatabase.DeleteItem(b);
                                    //    //                }
                                    //    //            }


                                    //    //        }
                                    //    //    }



                                    //    //}
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");

                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                    //    //        foreach (var TempAnsList in a)
                                    //    //        {
                                    //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    for (int c = 0; c < checkList.Count(); c++)
                                    //    //                    {
                                    //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                    //    //                        }
                                    //    //                    }
                                    //    //                }
                                    //    //            }

                                    //    //        }
                                    //    //    }


                                    //    //}
                                    //};
                                    //}
                                    check_box.CheckedChanged += async (s, e) =>
                                    {
                                        ////////Console.WriteLine("checkboxin1~~~");
                                        if (e.Value) // 如果選是，要跳出entry所以需要reset
                                        {
                                            //////////Console.WriteLine("IN~~~");
                                            //ischeck = true;
                                            //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                        //checkList2.RemoveAt(a);
                                                    }
                                                }

                                            }
                                            //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //if (tmp_name_list.Contains(questionList.ClientName))
                                            //{
                                            //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                            //    ////////Console.WriteLine("a~ " + total);
                                            //    tmp_name_list.Remove(questionList.ClientName);
                                            //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                            //    ////////Console.WriteLine("b~ " + total2);
                                            //}
                                            ////////Console.WriteLine("jjj~~~ " + j);
                                            if (j == "未發")
                                            {
                                                //IsChoose = true;
                                                CheckboxList[questionList.ClientName] = true;
                                                ChooseSaveToDB(questionList.ClientName, true);
                                                ////////Console.WriteLine("LLL~~~ " + IsChoose);
                                                ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                                            }
                                            //if (j == "是")
                                            //{
                                            //    ANS = 0;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else
                                            //{
                                            //    ANS = 1;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            ////////Console.WriteLine("j~~~ " + j);
                                            //qb03_count = 0;
                                            //foreach(var w in i.qb03)
                                            //{
                                            //    if(j == w)
                                            //    {
                                            //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            //        ////////Console.WriteLine("j~~ " + j);
                                            //        ////////Console.WriteLine("w~~~ " + w);

                                            //        ANS2 = Convert.ToString(qb03_count);
                                            //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            //    }
                                            //    qb03_count = qb03_count + 1;
                                            //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            //}
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                ////////Console.WriteLine("j00~~ " + j);
                                                ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (j == i.qb03[d])
                                                {

                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    ////////Console.WriteLine("j~~ " + j);
                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                                                    ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // ////////Console.WriteLine("cc~~~ " + p);
                                            ////////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var check3 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    qb_order = i.qb_order,
                                            //    wqb01 = ANS2 // 答案

                                            //};
                                            //////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //checkList2.Add(check3); // for save
                                            ////////Console.WriteLine("checkList2Add1~~~ ");

                                            //foreach (var k in checkList2)
                                            //{
                                            ////////Console.WriteLine("a~ " + k.wqh_s_num);
                                            ////////Console.WriteLine("b~ " + k.wqb01);
                                            //}
                                            ////////Console.WriteLine("count2~~ " + checkList2.Count());
                                            // 把問題選項存進資料庫
                                            //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                            ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                            ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                            //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                            //////////Console.WriteLine("j~~" + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                color = "Red";
                                            }
                                            else
                                            {
                                                color = "Green";
                                            }
                                            ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                            ////////Console.WriteLine("color~~~ " + color);
                                            ////////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                                            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                            
                                            ////////Console.WriteLine("j_HERE~~~ " + j);
                                            var check = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save

                                            ////////Console.WriteLine("wqb01~~ " + ANS2);
                                            ////////Console.WriteLine("j_HERE2~~~ " + j);
                                            ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                            checkList.Add(check); // for check

                                            //////////Console.WriteLine("CHECK" + checkList[0]);
                                            ////////Console.WriteLine("checkList2ADD~~ ");

                                            //if (j == "未發")
                                            //{
                                            //    ////Console.WriteLine("reset~friday~~ ");
                                            //    reset();
                                            //}
                                            reset();
                                            isReset = true;
                                            ////Console.WriteLine("ISREST~~~ " + isReset);
                                            // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                        }
                                        else //
                                        {
                                            //ischeck = false;
                                            ////////Console.WriteLine("remove~~~~~");
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                }
                                            }
                                        }

                                        //foreach (var b in checkList)
                                        //{
                                        //    ////////Console.WriteLine("HERE~~");
                                        //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                        //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                        //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                                        //    ////////Console.WriteLine("enrty : " + b.wqb99);
                                        //}
                                    };
                                    ////Console.WriteLine("isreset~~~ ");
                                    ////Console.WriteLine("isRest~~ " + isReset);
                                    if (isReset == true || isDB == true)
                                    {
                                        if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            ////Console.WriteLine("A~~~ " + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                ////Console.WriteLine("A-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Red,
                                                    FontSize = 20
                                                };
                                                isRed = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("A-2~~~ ");
                                                ////////Console.WriteLine("JKL1+1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            ////Console.WriteLine("B~~~~ " + j);
                                            if (j == "已發" || j == "否")
                                            {
                                                ////Console.WriteLine("B-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Green,
                                                    FontSize = 20
                                                };
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("B-2~~~ ");
                                                ////////Console.WriteLine("JKL2-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            ////Console.WriteLine("C~~~ " + j);
                                            ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                                            ////Console.WriteLine("j~~~ " + j);
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };

                                        }
                                    }
                                    else
                                    {
                                        ////Console.WriteLine("D~~~ ");
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }


                                    var stack_check = new StackLayout // checkbox跟選項
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children = { check_box, label_check }
                                    };

                                    //var ques_all_check = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_check, stack }
                                    //};

                                    //var stack_enrty = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Vertical,
                                    //    Children = { stack_check, entny }
                                    //};
                                    stack_ques.Children.Add(stack_check);

                                    //count = count + 1;

                                    //var final_stack = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_ques, label_que_name }
                                    //};
                                    //}
                                }
                                else if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期四") == true)
                                {
                                    //Console.WriteLine("thusday_in~~~ ");
                                    //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                    //{
                                    //count = 0;
                                    ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                                    ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                                    ////Console.WriteLine("order~~~ " + i.qb_order);
                                    ////Console.WriteLine("result_num~~~ " + result_num);
                                    TFcount = TFcount + 1;
                                    var temp_j = "";
                                    var temp_value = "";
                                    //var temp_j_map = "";
                                    //var temp_value_map = "";
                                    ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                                    if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                    {
                                        ////////Console.WriteLine("first~~ ");
                                        ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        var _wqhsnum = questionList.wqh_s_num;
                                        temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                        ////////Console.WriteLine("tempj~~ " + temp_j);
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            ////////Console.WriteLine("j00~~ " + j);
                                            ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (temp_j == i.qb03[d])
                                            {

                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                //Console.WriteLine("j~~ " + j);
                                                //Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                ////////Console.WriteLine("jj~~ " + temp_j);
                                                ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                                        //if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                        //{
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                            AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                            ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //}

                                        // ////////Console.WriteLine("cc~~~ " + p);
                                        ////////Console.WriteLine("ANS2~~ " + ANS2);
                                        //var ques = questionList.wqh_s_num + i.qb_order;
                                        //////////Console.WriteLine("ques!!! " + ques);
                                        //for(int e = 0; e < checkList2.Count(); e++)
                                        //{
                                        //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                        //    ////////Console.WriteLine("check!!! " + check);
                                        //    if (check != ques)
                                        //    {
                                        //        ////////Console.WriteLine("checkin!!! " + check);
                                        //        ////////Console.WriteLine("quesin!!! " + ques);
                                        //        var check3 = new checkInfo
                                        //        {
                                        //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //            qb_order = i.qb_order,
                                        //            wqb01 = ANS2 // 答案

                                        //        };
                                        //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        //        checkList2.Add(check3); // for save
                                        //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //    }
                                        //}
                                        ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                        //qb03_count = 0;
                                        //foreach (var y in i.qb03)
                                        //{
                                        //    ////////Console.WriteLine("yAA~~~ " + y);
                                        //    if (temp_j == y)
                                        //    {
                                        //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                        //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                        //        p = Convert.ToString(qb03_count);
                                        //    }
                                        //    qb03_count = qb03_count + 1;

                                        //}
                                        //////////Console.WriteLine("cc~~~ " + p);
                                        ////temp_value = TempAnsList.wqb99; // 之前的entry
                                        //var check2 = new checkInfo
                                        //{
                                        //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    wqb01 = p,// 答案
                                        //              //wqb99 = TempAnsList.wqb99

                                        //};
                                    }
                                    // 跑選是的reset把checkList抓回來判斷
                                    ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                    for (int a = 0; a < checkList.Count(); a++)
                                    {
                                        ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                        ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                        //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        {
                                            //////////Console.WriteLine("IMMMM222~~~~");
                                            ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                            {
                                                ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                //foreach (var w in i.qb03)
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                    ////////Console.WriteLine("w00~~~ " + d.ToString());
                                                    if (checkList[a].wqb01 == d.ToString())
                                                    {

                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        ////////Console.WriteLine("j~~ " + j);
                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        temp_j = i.qb03[d]; // 答案
                                                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                                    }

                                                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // ////////Console.WriteLine("cc~~~ " + p);
                                                ////////Console.WriteLine("ANS2~~ " + ANS2);

                                                //temp_value = checkList[a].wqb99; // entry
                                            }
                                        }
                                    }
                                    ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                                    ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                                    ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                                    // ------------------------------------------

                                    //////////Console.WriteLine("")
                                    //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //~~~~~~~~~~~~~~~~~~~~~~~~
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                    ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                                    ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                                    ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                                    ////////////Console.WriteLine("ID" + TempAnsList.ID);
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    //checkList.Add(check2);
                                    //checkList2.Add(check2);
                                    //~~~~~~~~~~~~~~~~~~~~`
                                    ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                                    ////////Console.WriteLine("j~~ " + j);

                                    bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                 //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                    if (ischeck == true)
                                    {
                                        //if (j == "是" || j == "已發")
                                        //{
                                        //    isRed = true;
                                        //}
                                        //else
                                        //{
                                        //    isGreen = true;
                                        //}

                                    }
                                    ////////Console.WriteLine("j1~~~ " + j);
                                    ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                                    ////////Console.WriteLine("isRed1~~~ " + isRed);
                                    ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                                    ////////Console.WriteLine("TFcount~~~" + TFcount);
                                    //if (j == "是" || j == "已發")
                                    ////////Console.WriteLine("name~~ " + questionList.ClientName);
                                    ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (TFcount == 1)
                                    {
                                        ////////Console.WriteLine("Red~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Green
                                        };
                                    }
                                    else
                                    {
                                        ////////Console.WriteLine("Green~~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Red
                                        };
                                    }



                                    //if (j == "是")
                                    //{
                                    //entny = new Entry // 產生Entry
                                    //{
                                    //    Placeholder = "請說明",
                                    //    Text = temp_value,
                                    //    IsVisible = isEntry,
                                    //    IsEnabled = isEntry


                                    //};



                                    //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                    //{
                                    //    for (int a = 0; a < checkList.Count(); a++)
                                    //    {
                                    //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                    //        {
                                    //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                    //            {
                                    //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                    //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                    //                checkList[a].wqb99 = ee.NewTextValue;
                                    //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                                    //            }

                                    //        }

                                    //    }
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");
                                    //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                    //    //        foreach (var TempAnsList in c)
                                    //    //        {
                                    //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                    //    //            ////////Console.WriteLine("WHO~~" + who);
                                    //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    for (int d = 0; d < checkList.Count(); d++)
                                    //    //                    {
                                    //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                    //    //                        }

                                    //    //                    }

                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    //temp_value = TempAnsList.wqb99;
                                    //    //                    //var check2 = new checkInfo
                                    //    //                    //{
                                    //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                    //    //                    //    wqb99 = TempAnsList.wqb99

                                    //    //                    //};

                                    //    //                    //checkList.Add(check2);

                                    //    //                    //MapView.AccDatabase.DeleteItem(b);
                                    //    //                }
                                    //    //            }


                                    //    //        }
                                    //    //    }



                                    //    //}
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");

                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                    //    //        foreach (var TempAnsList in a)
                                    //    //        {
                                    //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    for (int c = 0; c < checkList.Count(); c++)
                                    //    //                    {
                                    //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                    //    //                        }
                                    //    //                    }
                                    //    //                }
                                    //    //            }

                                    //    //        }
                                    //    //    }


                                    //    //}
                                    //};
                                    //}
                                    check_box.CheckedChanged += async (s, e) =>
                                    {
                                        ////////Console.WriteLine("checkboxin1~~~");
                                        if (e.Value) // 如果選是，要跳出entry所以需要reset
                                        {
                                            //////////Console.WriteLine("IN~~~");
                                            //ischeck = true;
                                            //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                        //checkList2.RemoveAt(a);
                                                    }
                                                }

                                            }
                                            //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //if (tmp_name_list.Contains(questionList.ClientName))
                                            //{
                                            //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                            //    ////////Console.WriteLine("a~ " + total);
                                            //    tmp_name_list.Remove(questionList.ClientName);
                                            //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                            //    ////////Console.WriteLine("b~ " + total2);
                                            //}
                                            ////////Console.WriteLine("jjj~~~ " + j);
                                            if (j == "未發")
                                            {
                                                //IsChoose = true;
                                                CheckboxList[questionList.ClientName] = true;
                                                ChooseSaveToDB(questionList.ClientName, true);
                                                ////////Console.WriteLine("LLL~~~ " + IsChoose);
                                                ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                                            }
                                            //if (j == "是")
                                            //{
                                            //    ANS = 0;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else
                                            //{
                                            //    ANS = 1;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            ////////Console.WriteLine("j~~~ " + j);
                                            //qb03_count = 0;
                                            //foreach(var w in i.qb03)
                                            //{
                                            //    if(j == w)
                                            //    {
                                            //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            //        ////////Console.WriteLine("j~~ " + j);
                                            //        ////////Console.WriteLine("w~~~ " + w);

                                            //        ANS2 = Convert.ToString(qb03_count);
                                            //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            //    }
                                            //    qb03_count = qb03_count + 1;
                                            //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            //}
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                ////////Console.WriteLine("j00~~ " + j);
                                                ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (j == i.qb03[d])
                                                {

                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    ////////Console.WriteLine("j~~ " + j);
                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                                                    ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // ////////Console.WriteLine("cc~~~ " + p);
                                            ////////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var check3 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    qb_order = i.qb_order,
                                            //    wqb01 = ANS2 // 答案

                                            //};
                                            //////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //checkList2.Add(check3); // for save
                                            ////////Console.WriteLine("checkList2Add1~~~ ");

                                            //foreach (var k in checkList2)
                                            //{
                                            ////////Console.WriteLine("a~ " + k.wqh_s_num);
                                            ////////Console.WriteLine("b~ " + k.wqb01);
                                            //}
                                            ////////Console.WriteLine("count2~~ " + checkList2.Count());
                                            // 把問題選項存進資料庫
                                            //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                            ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                            ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                            //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                            //////////Console.WriteLine("j~~" + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                color = "Red";
                                            }
                                            else
                                            {
                                                color = "Green";
                                            }
                                            ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                            //Console.WriteLine("color~~~ " + color);
                                            //Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                                            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);

                                            //Console.WriteLine("j_HERE~~~ " + j);
                                            var check = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save

                                            ////////Console.WriteLine("wqb01~~ " + ANS2);
                                            ////////Console.WriteLine("j_HERE2~~~ " + j);
                                            ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                            checkList.Add(check); // for check

                                            //////////Console.WriteLine("CHECK" + checkList[0]);
                                            ////////Console.WriteLine("checkList2ADD~~ ");

                                            //if (j == "未發")
                                            //{
                                            //    ////Console.WriteLine("reset~friday~~ ");
                                            //    reset();
                                            //}
                                            reset();
                                            isReset = true;
                                            ////Console.WriteLine("ISREST~~~ " + isReset);
                                            // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                        }
                                        else //
                                        {
                                            //ischeck = false;
                                            ////////Console.WriteLine("remove~~~~~");
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                }
                                            }
                                        }

                                        //foreach (var b in checkList)
                                        //{
                                        //    ////////Console.WriteLine("HERE~~");
                                        //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                        //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                        //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                                        //    ////////Console.WriteLine("enrty : " + b.wqb99);
                                        //}
                                    };
                                    ////Console.WriteLine("isreset~~~ ");
                                    ////Console.WriteLine("isRest~~ " + isReset);
                                    //Console.WriteLine("bingoname22~~~ " + questionList.wqh_s_num);
                                    //Console.WriteLine("qborder22~~ " + i.qb_order);
                                    //Console.WriteLine("color22~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (isReset == true || isDB == true)
                                    {
                                        if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //Console.WriteLine("A~~~ " + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                ////Console.WriteLine("A-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Red,
                                                    FontSize = 20
                                                };
                                                isRed = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("A-2~~~ ");
                                                ////////Console.WriteLine("JKL1+1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //Console.WriteLine("B~~~~ " + j);
                                            if (j == "已發" || j == "否")
                                            {
                                                //Console.WriteLine("B-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Green,
                                                    FontSize = 20
                                                };
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("B-2~~~ ");
                                                ////////Console.WriteLine("JKL2-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            ////Console.WriteLine("C~~~ " + j);
                                            ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                                            ////Console.WriteLine("j~~~ " + j);
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };

                                        }
                                    }
                                    else
                                    {
                                        ////Console.WriteLine("D~~~ ");
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }


                                    var stack_check = new StackLayout // checkbox跟選項
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children = { check_box, label_check }
                                    };

                                    //var ques_all_check = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_check, stack }
                                    //};

                                    //var stack_enrty = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Vertical,
                                    //    Children = { stack_check, entny }
                                    //};
                                    stack_ques.Children.Add(stack_check);

                                    //count = count + 1;

                                    //var final_stack = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_ques, label_que_name }
                                    //};
                                    //}
                                }
                                else
                                {
                                    if(i.qb_order == "1")
                                    {
                                        //Console.WriteLine("qborder1~~in~~~");
                                        //Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                                        //Console.WriteLine("name~~ " + questionList.ClientName);
                                        //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                        //{
                                        //count = 0;
                                        ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                                        ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                                        ////Console.WriteLine("order~~~ " + i.qb_order);
                                        ////Console.WriteLine("result_num~~~ " + result_num);
                                        TFcount = TFcount + 1;
                                        var temp_j = "";
                                        var temp_value = "";
                                        //var temp_j_map = "";
                                        //var temp_value_map = "";

                                        // 跑選是的reset把checkList抓回來判斷
                                        if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                        {
                                            //////Console.WriteLine("third~~ ");
                                            //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            var _wqhsnum = questionList.wqh_s_num;
                                            temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                            //////Console.WriteLine("tempj~~ " + temp_j);
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("j00~~ " + j);
                                                //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (temp_j == i.qb03[d])
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    //////Console.WriteLine("jj~~ " + temp_j);
                                                    //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                                                    //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                                                                    //{
                                                                    //    //////Console.WriteLine("ans2~~~ " + ANS2);
                                                                    //    var check3 = new checkInfo
                                                                    //    {
                                                                    //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                                    //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                                    //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                                    //        qb_order = i.qb_order,
                                                                    //        wqb01 = ANS2 // 答案

                                            //    };
                                            //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //    checkList2.Add(check3); // for save
                                            //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                                            //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                                            //    //////Console.WriteLine("checkList2Add99~~~ ");
                                            //}

                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var ques = questionList.wqh_s_num + i.qb_order;
                                            ////////Console.WriteLine("ques!!! " + ques);
                                            //for(int e = 0; e < checkList2.Count(); e++)
                                            //{
                                            //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                            //    //////Console.WriteLine("check!!! " + check);
                                            //    if (check != ques)
                                            //    {
                                            //        //////Console.WriteLine("checkin!!! " + check);
                                            //        //////Console.WriteLine("quesin!!! " + ques);
                                            //        var check3 = new checkInfo
                                            //        {
                                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //            qb_order = i.qb_order,
                                            //            wqb01 = ANS2 // 答案

                                            //        };
                                            //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //        checkList2.Add(check3); // for save
                                            //        //////Console.WriteLine("checkList2Add2~~~ ");
                                            //    }
                                            //}
                                            //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                            //qb03_count = 0;
                                            //foreach (var y in i.qb03)
                                            //{
                                            //    //////Console.WriteLine("yAA~~~ " + y);
                                            //    if (temp_j == y)
                                            //    {
                                            //        //////Console.WriteLine("temp_j~~ " + temp_j);
                                            //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                                            //        p = Convert.ToString(qb03_count);
                                            //    }
                                            //    qb03_count = qb03_count + 1;

                                            //}
                                            ////////Console.WriteLine("cc~~~ " + p);
                                            ////temp_value = TempAnsList.wqb99; // 之前的entry
                                            //var check2 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    wqb01 = p,// 答案
                                            //              //wqb99 = TempAnsList.wqb99

                                            //};
                                        }
                                        // 跑選是的reset把checkList抓回來判斷
                                        //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                            //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                            {
                                                ////////Console.WriteLine("IMMMM222~~~~");
                                                //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                                {
                                                    //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                    //foreach (var w in i.qb03)
                                                    for (int d = 0; d < i.qb03.Count(); d++)
                                                    {
                                                        //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                        //////Console.WriteLine("w00~~~ " + d.ToString());
                                                        if (checkList[a].wqb01 == d.ToString())
                                                        {

                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                            //////Console.WriteLine("j~~ " + j);
                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //ANS2 = Convert.ToString(qb03_count);
                                                            temp_j = i.qb03[d]; // 答案
                                                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                        }

                                                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                    }
                                                    // //////Console.WriteLine("cc~~~ " + p);
                                                    //////Console.WriteLine("ANS2~~ " + ANS2);

                                                    //temp_value = checkList[a].wqb99; // entry
                                                }
                                            }
                                        }



                                        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                     //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                        if (ischeck == true)
                                        {
                                            if (j == "是" || j == "已發")
                                            {
                                                isRed = true;
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                isGreen = true;
                                                isRed = false;
                                            }
                                        }
                                        //////Console.WriteLine("j3~~~ " + j);
                                        //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                                        //////Console.WriteLine("isRed3~~~ " + isRed);
                                        //////Console.WriteLine("isGreen3~~~ " + isGreen);

                                        //////Console.WriteLine("TFcount~~~" + TFcount);
                                        if (TFcount == 1)
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Red,

                                            };
                                        }
                                        else
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Green
                                            };
                                        }



                                        //if (j == "是")
                                        //{
                                        //entny = new Entry // 產生Entry
                                        //{
                                        //    Placeholder = "請說明",
                                        //    Text = temp_value,
                                        //    IsVisible = isEntry,
                                        //    IsEnabled = isEntry


                                        //};



                                        //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                        //{
                                        //    for (int a = 0; a < checkList.Count(); a++)
                                        //    {
                                        //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                        //        {
                                        //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                        //            {
                                        //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                        //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                        //                checkList[a].wqb99 = ee.NewTextValue;
                                        //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                        //            }

                                        //        }

                                        //    }
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");
                                        //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                        //    //        foreach (var TempAnsList in c)
                                        //    //        {
                                        //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                        //    //            //////Console.WriteLine("WHO~~" + who);
                                        //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    for (int d = 0; d < checkList.Count(); d++)
                                        //    //                    {
                                        //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                        //    //                        }

                                        //    //                    }

                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    //temp_value = TempAnsList.wqb99;
                                        //    //                    //var check2 = new checkInfo
                                        //    //                    //{
                                        //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                        //    //                    //    wqb99 = TempAnsList.wqb99

                                        //    //                    //};

                                        //    //                    //checkList.Add(check2);

                                        //    //                    //MapView.AccDatabase.DeleteItem(b);
                                        //    //                }
                                        //    //            }


                                        //    //        }
                                        //    //    }



                                        //    //}
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");

                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                        //    //        foreach (var TempAnsList in a)
                                        //    //        {
                                        //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    for (int c = 0; c < checkList.Count(); c++)
                                        //    //                    {
                                        //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                        //    //                        }
                                        //    //                    }
                                        //    //                }
                                        //    //            }

                                        //    //        }
                                        //    //    }


                                        //    //}
                                        //};
                                        //}
                                        check_box.CheckedChanged += async (s, e) =>
                                        {
                                            //////Console.WriteLine("checkboxin3~~~");
                                            if (e.Value) // 如果選是，要跳出entry所以需要reset
                                            {
                                                ////////Console.WriteLine("IN~~~");
                                                //ischeck = true;
                                                //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                    {
                                                        if (checkList[a].qb_s_num == i.qb_s_num)
                                                        {
                                                            checkList.RemoveAt(a);
                                                            //checkList2.RemoveAt(a);
                                                        }
                                                    }

                                                }
                                                ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //if (tmp_name_list.Contains(questionList.ClientName))
                                                //{
                                                //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                                //    //////Console.WriteLine("a~ " + total);
                                                //    tmp_name_list.Remove(questionList.ClientName);
                                                //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                                //    //////Console.WriteLine("b~ " + total2);
                                                //}
                                                //////Console.WriteLine("jjj~~~ " + j);
                                                if (j == "未發")
                                                {
                                                    //IsChoose = true;
                                                    CheckboxList[questionList.ClientName] = true;
                                                    ChooseSaveToDB(questionList.ClientName, true);
                                                    //////Console.WriteLine("LLL~~~ " + IsChoose);
                                                }
                                                //if (j == "是" || j == "已發")
                                                //{
                                                //    ANS = 0;
                                                //    ANS2 = Convert.ToString(ANS);


                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS2 = Convert.ToString(ANS);

                                                //}
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    //Console.WriteLine("j00~~ " + j);
                                                    //Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                    if (j == i.qb03[d])
                                                    {

                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                        //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        //////Console.WriteLine("j~~ " + j);
                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        ANS2 = d.ToString();
                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                        //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                    }

                                                    //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS2~~ " + ANS2);
                                                //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS21~~~ " + ANS2);
                                                // 把問題選項存進資料庫
                                                ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                                //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                                //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                                ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                                //Console.WriteLine("j~~" + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    //Console.WriteLine("G_in~~~ ");
                                                    color = "Green";
                                                }
                                                else
                                                {
                                                    //Console.WriteLine("R_in~~~ ");
                                                    color = "Red";
                                                }
                                                //////Console.WriteLine("color~~~ " + color);
                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                                ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    wqb01 = ANS2 // 答案

                                                };
                                                checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    qb_order = i.qb_order,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                                checkList2.Add(check3); // for save
                                                                        //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList.Add(check); // for check
                                                                      //checkList2.Add(check3); // for save
                                                                      ////////Console.WriteLine("CHECK" + checkList[0]);
                                                ///
                                                reset();
                                                //isReset = true;
                                                //if (j == "未發")
                                                //{
                                                //    reset();
                                                //}


                                                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                            }
                                            else // 選否則不用reset
                                            {
                                                //ischeck = false;
                                                //////Console.WriteLine("remove~~~~~");
                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                    }
                                                }
                                            }

                                            //foreach (var b in checkList)
                                            //{
                                            //    //////Console.WriteLine("HERE~~");
                                            //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                            //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                            //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                            //    //////Console.WriteLine("enrty : " + b.wqb99);
                                            //}
                                        };
                                        //////Console.WriteLine("isReset~~~~ " + isReset);
                                        //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                                        //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        //////Console.WriteLine("WWWRRR_j~~~ " + j);
                                        ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        if (isReset == true || isDB == true)
                                        {
                                            if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //Console.WriteLine("JKL1~~~ " + j);
                                                if (j == "是")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Green,
                                                        //Margin = new Thickness(-10,0,0,0),
                                                        FontSize = 20
                                                    };
                                                    isRed = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL1-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL2~~~ " + j);
                                                if (j == "否")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Red,
                                                        //Margin = new Thickness(-10, 0, 0, 0),
                                                        FontSize = 20
                                                    };
                                                    isGreen = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL2-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL3~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };
                                        }


                                        var stack_check = new StackLayout // checkbox跟選項
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Children = { check_box, label_check }
                                        };

                                        //var ques_all_check = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_check, stack }
                                        //};

                                        //var stack_enrty = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Vertical,
                                        //    Children = { stack_check, entny }
                                        //};
                                        stack_ques.Children.Add(stack_check);
                                        //count = count + 1;

                                        //var final_stack = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_ques, label_que_name }
                                        //};
                                        //}
                                    }
                                    else 
                                    {
                                        //Console.WriteLine("qborder_2_in~~~ ");
                                        //Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                                        //Console.WriteLine("name~~ " + questionList.ClientName);
                                        //////Console.WriteLine("CVB~~~~");
                                        //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                        //{
                                        //count = 0;
                                        ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                                        ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                                        ////Console.WriteLine("order~~~ " + i.qb_order);
                                        ////Console.WriteLine("result_num~~~ " + result_num);
                                        TFcount = TFcount + 1;
                                        var temp_j = "";
                                        var temp_value = "";
                                        //var temp_j_map = "";
                                        //var temp_value_map = "";

                                        // 跑選是的reset把checkList抓回來判斷
                                        if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                        {
                                            //////Console.WriteLine("third~~ ");
                                            //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            var _wqhsnum = questionList.wqh_s_num;
                                            temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                            //////Console.WriteLine("tempj~~ " + temp_j);
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("j00~~ " + j);
                                                //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (temp_j == i.qb03[d])
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    //////Console.WriteLine("jj~~ " + temp_j);
                                                    //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                                                    //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                                                                    //{
                                                                    //    //////Console.WriteLine("ans2~~~ " + ANS2);
                                                                    //    var check3 = new checkInfo
                                                                    //    {
                                                                    //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                                    //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                                    //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                                    //        qb_order = i.qb_order,
                                                                    //        wqb01 = ANS2 // 答案

                                            //    };
                                            //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //    checkList2.Add(check3); // for save
                                            //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                                            //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                                            //    //////Console.WriteLine("checkList2Add99~~~ ");
                                            //}

                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var ques = questionList.wqh_s_num + i.qb_order;
                                            ////////Console.WriteLine("ques!!! " + ques);
                                            //for(int e = 0; e < checkList2.Count(); e++)
                                            //{
                                            //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                            //    //////Console.WriteLine("check!!! " + check);
                                            //    if (check != ques)
                                            //    {
                                            //        //////Console.WriteLine("checkin!!! " + check);
                                            //        //////Console.WriteLine("quesin!!! " + ques);
                                            //        var check3 = new checkInfo
                                            //        {
                                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //            qb_order = i.qb_order,
                                            //            wqb01 = ANS2 // 答案

                                            //        };
                                            //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //        checkList2.Add(check3); // for save
                                            //        //////Console.WriteLine("checkList2Add2~~~ ");
                                            //    }
                                            //}
                                            //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                            //qb03_count = 0;
                                            //foreach (var y in i.qb03)
                                            //{
                                            //    //////Console.WriteLine("yAA~~~ " + y);
                                            //    if (temp_j == y)
                                            //    {
                                            //        //////Console.WriteLine("temp_j~~ " + temp_j);
                                            //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                                            //        p = Convert.ToString(qb03_count);
                                            //    }
                                            //    qb03_count = qb03_count + 1;

                                            //}
                                            ////////Console.WriteLine("cc~~~ " + p);
                                            ////temp_value = TempAnsList.wqb99; // 之前的entry
                                            //var check2 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    wqb01 = p,// 答案
                                            //              //wqb99 = TempAnsList.wqb99

                                            //};
                                        }
                                        // 跑選是的reset把checkList抓回來判斷
                                        //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                            //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                            {
                                                ////////Console.WriteLine("IMMMM222~~~~");
                                                //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                                {
                                                    //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                    //foreach (var w in i.qb03)
                                                    for (int d = 0; d < i.qb03.Count(); d++)
                                                    {
                                                        //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                        //////Console.WriteLine("w00~~~ " + d.ToString());
                                                        if (checkList[a].wqb01 == d.ToString())
                                                        {

                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                            //////Console.WriteLine("j~~ " + j);
                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //ANS2 = Convert.ToString(qb03_count);
                                                            temp_j = i.qb03[d]; // 答案
                                                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                        }

                                                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                    }
                                                    // //////Console.WriteLine("cc~~~ " + p);
                                                    //////Console.WriteLine("ANS2~~ " + ANS2);

                                                    //temp_value = checkList[a].wqb99; // entry
                                                }
                                            }
                                        }



                                        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                     //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                        if (ischeck == true)
                                        {
                                            if (j == "是" || j == "已發")
                                            {
                                                isRed = true;
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                isGreen = true;
                                                isRed = false;
                                            }
                                        }
                                        //////Console.WriteLine("j3~~~ " + j);
                                        //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                                        //////Console.WriteLine("isRed3~~~ " + isRed);
                                        //////Console.WriteLine("isGreen3~~~ " + isGreen);

                                        //////Console.WriteLine("TFcount~~~" + TFcount);
                                        if (TFcount == 1)
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Red,

                                            };
                                        }
                                        else
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Green
                                            };
                                        }



                                        //if (j == "是")
                                        //{
                                        //entny = new Entry // 產生Entry
                                        //{
                                        //    Placeholder = "請說明",
                                        //    Text = temp_value,
                                        //    IsVisible = isEntry,
                                        //    IsEnabled = isEntry


                                        //};



                                        //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                        //{
                                        //    for (int a = 0; a < checkList.Count(); a++)
                                        //    {
                                        //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                        //        {
                                        //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                        //            {
                                        //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                        //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                        //                checkList[a].wqb99 = ee.NewTextValue;
                                        //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                        //            }

                                        //        }

                                        //    }
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");
                                        //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                        //    //        foreach (var TempAnsList in c)
                                        //    //        {
                                        //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                        //    //            //////Console.WriteLine("WHO~~" + who);
                                        //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    for (int d = 0; d < checkList.Count(); d++)
                                        //    //                    {
                                        //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                        //    //                        }

                                        //    //                    }

                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    //temp_value = TempAnsList.wqb99;
                                        //    //                    //var check2 = new checkInfo
                                        //    //                    //{
                                        //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                        //    //                    //    wqb99 = TempAnsList.wqb99

                                        //    //                    //};

                                        //    //                    //checkList.Add(check2);

                                        //    //                    //MapView.AccDatabase.DeleteItem(b);
                                        //    //                }
                                        //    //            }


                                        //    //        }
                                        //    //    }



                                        //    //}
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");

                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                        //    //        foreach (var TempAnsList in a)
                                        //    //        {
                                        //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    for (int c = 0; c < checkList.Count(); c++)
                                        //    //                    {
                                        //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                        //    //                        }
                                        //    //                    }
                                        //    //                }
                                        //    //            }

                                        //    //        }
                                        //    //    }


                                        //    //}
                                        //};
                                        //}
                                        check_box.CheckedChanged += async (s, e) =>
                                        {
                                            //////Console.WriteLine("checkboxin3~~~");
                                            if (e.Value) // 如果選是，要跳出entry所以需要reset
                                            {
                                                ////////Console.WriteLine("IN~~~");
                                                //ischeck = true;
                                                //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                    {
                                                        if (checkList[a].qb_s_num == i.qb_s_num)
                                                        {
                                                            checkList.RemoveAt(a);
                                                            //checkList2.RemoveAt(a);
                                                        }
                                                    }

                                                }
                                                ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //if (tmp_name_list.Contains(questionList.ClientName))
                                                //{
                                                //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                                //    //////Console.WriteLine("a~ " + total);
                                                //    tmp_name_list.Remove(questionList.ClientName);
                                                //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                                //    //////Console.WriteLine("b~ " + total2);
                                                //}
                                                //////Console.WriteLine("jjj~~~ " + j);
                                                if (j == "未發")
                                                {
                                                    //IsChoose = true;
                                                    CheckboxList[questionList.ClientName] = true;
                                                    ChooseSaveToDB(questionList.ClientName, true);
                                                    //////Console.WriteLine("LLL~~~ " + IsChoose);
                                                }
                                                //if (j == "是" || j == "已發")
                                                //{
                                                //    ANS = 0;
                                                //    ANS2 = Convert.ToString(ANS);


                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS2 = Convert.ToString(ANS);

                                                //}
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    //////Console.WriteLine("j00~~ " + j);
                                                    //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                    if (j == i.qb03[d])
                                                    {

                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                        //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        //////Console.WriteLine("j~~ " + j);
                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        ANS2 = d.ToString();
                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                        //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                    }

                                                    //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS2~~ " + ANS2);
                                                //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS21~~~ " + ANS2);
                                                // 把問題選項存進資料庫
                                                ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                                //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                                //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                                ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                                ////////Console.WriteLine("j~~" + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    color = "Red";
                                                }
                                                else
                                                {
                                                    color = "Green";
                                                }
                                                //////Console.WriteLine("color~~~ " + color);
                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                                ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    wqb01 = ANS2 // 答案

                                                };
                                                checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    qb_order = i.qb_order,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                                checkList2.Add(check3); // for save
                                                                        //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList.Add(check); // for check
                                                                      //checkList2.Add(check3); // for save
                                                                      ////////Console.WriteLine("CHECK" + checkList[0]);
                                                ///
                                                reset();
                                                //isReset = true;
                                                //if (j == "未發")
                                                //{
                                                //    reset();
                                                //}


                                                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                            }
                                            else // 選否則不用reset
                                            {
                                                //ischeck = false;
                                                //////Console.WriteLine("remove~~~~~");
                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                    }
                                                }
                                            }

                                            //foreach (var b in checkList)
                                            //{
                                            //    //////Console.WriteLine("HERE~~");
                                            //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                            //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                            //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                            //    //////Console.WriteLine("enrty : " + b.wqb99);
                                            //}
                                        };
                                        //////Console.WriteLine("isReset~~~~ " + isReset);
                                        //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                                        //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        //////Console.WriteLine("WWWRRR_j~~~ " + j);
                                        ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        if (isReset == true || isDB == true)
                                        {
                                            if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL1~~~ " + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Red,
                                                        //Margin = new Thickness(-10,0,0,0),
                                                        FontSize = 20
                                                    };
                                                    isRed = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL1-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL2~~~ " + j);
                                                if (j == "未發" || j == "否")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Green,
                                                        //Margin = new Thickness(-10, 0, 0, 0),
                                                        FontSize = 20
                                                    };
                                                    isGreen = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL2-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL3~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };
                                        }


                                        var stack_check = new StackLayout // checkbox跟選項
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Children = { check_box, label_check }
                                        };

                                        //var ques_all_check = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_check, stack }
                                        //};

                                        //var stack_enrty = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Vertical,
                                        //    Children = { stack_check, entny }
                                        //};
                                        stack_ques.Children.Add(stack_check);
                                        //count = count + 1;

                                        //var final_stack = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_ques, label_que_name }
                                        //};
                                        //}
                                    }
                                   
                                }
                            }
                        }
                        
                        

                        
                        quesStack.Children.Add(stack); // w

                        //quesStack.Children.Add(final_stack);
                        //quesStack.Children.Add(label_que_name);
                        //quesStack.Children.Add(stack_ques);
                        var final_stack = new StackLayout
                        {
                            
                            Orientation = StackOrientation.Vertical,
                            Children = { label_que_name, stack_ques }
                        };


                        Frame frame = new Frame // frame包上面那個stacklayout
                        {
                            Padding = new Thickness(10, 5, 10, 5),
                           Margin= new Thickness(5, 0, 5, 0),
                            BackgroundColor = Color.FromHex("eddcd2"),

                            CornerRadius = 10,
                            HasShadow = false,
                            Content = final_stack
                        };
                        //ScrollView scrollView = new ScrollView { 
                        //    Content = frame ,
                        //    Orientation = ScrollOrientation.Both
                        //};

                        quesStack.Children.Add(frame);

                        //var last_stack = new StackLayout
                        //{
                        //    Orientation = StackOrientation.Vertical,
                        //    Children = { stack, frame }
                        //};

                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                        //Content = scrollView;
                        //ScrollView scrollView = new ScrollView
                        //{
                        //    Content = quesStack

                        //};
                        //quesStack.Children.Add(scrollView);
                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                    }
                    
                }
                else if (Int32.Parse(i.qb_order) < 4 && CheckboxList[questionList.ClientName] == true)
                {
                    ////////Console.WriteLine("inA~~~~ ");
                    if (i.qb02 == "1") // 問題類型(假設1是是否題 / 單選)(沒有entry版本)
                    {
                        Qtype = "1";
                        //string set = questionList.wqh_s_num + i.qb_s_num;
                        //TmpCheckList[set] = false;
                        var label_que_name = new Label // 問題題號+題目
                        {
                            Text = i.qb_order + " " + i.qb01,
                            FontSize = 20,
                            TextColor = Color.Black
                        };

                        var stack_ques = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal
                        };
                        ////////Console.WriteLine("i.qb03~~~ " + i.qb03);
                        ////////Console.WriteLine("i.qb_order~~~ " + i.qb_order);
                        ////////Console.WriteLine("result~~~ " + result);
                        ////////Console.WriteLine("TorF~~~" + result == "星期三");
                        ////////Console.WriteLine("TorF2~~~~ " + result.Equals("星期三"));

                        ////////Console.WriteLine("value~~ " + questionList.wqh_s_num);
                        ////////Console.WriteLine("checklist2~count0~ " + checkList2.Count());
                        if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result_num < 3)
                        {
                            ////Console.WriteLine("nameBB~~~ " + questionList.ClientName);
                            ////Console.WriteLine("countBB~~~ " + questionList.qbs.Count());
                            ////Console.WriteLine("order~~~ " + i.qb_order);
                            ////Console.WriteLine("result_num~~~ " + result_num);
                            ////////Console.WriteLine("reseultnum~~LA~~~ " + result_num);
                            ////////Console.WriteLine("LKJ~~~~~~ ");
                            string[] list = { "已發" };
                            foreach (var j in list) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            {
                                //////Console.WriteLine("LKJH~~~ " + j);
                                //count = 0;
                                TFcount = TFcount + 1;
                                var temp_j = "";
                                var temp_value = "";
                                //var temp_j_map = "";
                                //var temp_value_map = "";

                                // 跑選是的reset把checkList抓回來判斷
                                if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                {
                                    ////////Console.WriteLine("first~~ ");
                                    ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    var _wqhsnum = questionList.wqh_s_num;
                                    temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                    ////////Console.WriteLine("tempj~~ " + temp_j);
                                    for (int d = 0; d < i.qb03.Count(); d++)
                                    {
                                        ////////Console.WriteLine("j00~~ " + j);
                                        ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                        if (temp_j == i.qb03[d])
                                        {

                                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                            //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                            ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            ////////Console.WriteLine("j~~ " + j);
                                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                            //ANS2 = Convert.ToString(qb03_count);
                                            ANS2 = d.ToString();
                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                            ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                        }

                                        ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                    }
                                    ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]);
                                    if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                    {
                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = ANS2 // 答案

                                        };
                                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        checkList2.Add(check3); // for save
                                        AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                        TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                        ////////Console.WriteLine("checkList2Add2~~~ ");
                                    }

                                    // ////////Console.WriteLine("cc~~~ " + p);
                                    ////////Console.WriteLine("ANS2~~ " + ANS2);
                                    //var ques = questionList.wqh_s_num + i.qb_order;
                                    //////////Console.WriteLine("ques!!! " + ques);
                                    //for(int e = 0; e < checkList2.Count(); e++)
                                    //{
                                    //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                    //    ////////Console.WriteLine("check!!! " + check);
                                    //    if (check != ques)
                                    //    {
                                    //        ////////Console.WriteLine("checkin!!! " + check);
                                    //        ////////Console.WriteLine("quesin!!! " + ques);
                                    //        var check3 = new checkInfo
                                    //        {
                                    //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //            qb_order = i.qb_order,
                                    //            wqb01 = ANS2 // 答案

                                    //        };
                                    //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                    //        checkList2.Add(check3); // for save
                                    //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                    //    }
                                    //}
                                    ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                }
                                // 跑選是的reset把checkList抓回來判斷
                                ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                for (int a = 0; a < checkList.Count(); a++)
                                {
                                    ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                    ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    {
                                        ////////Console.WriteLine("IMMMM222~~~~");
                                        //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                        if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                        {
                                            //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                            //foreach (var w in i.qb03)
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                //////Console.WriteLine("w00~~~ " + d.ToString());
                                                if (checkList[a].wqb01 == d.ToString())
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    temp_j = i.qb03[d]; // 答案
                                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);

                                            //temp_value = checkList[a].wqb99; // entry
                                        }
                                    }
                                }



                                bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                             //IsChoose = (temp_j == "未發") ? true : false; // 如果答案是 是 -> entry顯示
                                if (ischeck == true)
                                {
                                    if (j == "是" || j == "已發")
                                    {
                                        isRed = true;
                                    }
                                    else
                                    {
                                        isGreen = true;
                                    }
                                }
                                //////Console.WriteLine("j2~~~ " + j);
                                //////Console.WriteLine("isckeck2~~~~ " + ischeck);
                                //////Console.WriteLine("isRed2~~~ " + isRed);
                                //////Console.WriteLine("isGreen2~~~ " + isGreen);


                                //////Console.WriteLine("TFcount~~~" + TFcount);
                                if (TFcount == 1)
                                {
                                    check_box = new CheckBox // 產生checkbox
                                    {

                                        IsChecked = ischeck,
                                        Margin = new Thickness(-5, 0, 0, 0),
                                        //Color = Color.FromHex("264653")
                                        Color = Color.Red
                                    };
                                }
                                else
                                {
                                    check_box = new CheckBox // 產生checkbox
                                    {

                                        IsChecked = ischeck,
                                        Margin = new Thickness(-5, 0, 0, 0),
                                        //Color = Color.FromHex("264653")
                                        Color = Color.Green
                                    };
                                }



                                //if (j == "是")
                                //{
                                //entny = new Entry // 產生Entry
                                //{
                                //    Placeholder = "請說明",
                                //    Text = temp_value,
                                //    IsVisible = isEntry,
                                //    IsEnabled = isEntry


                                //};



                                //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                //{
                                //    for (int a = 0; a < checkList.Count(); a++)
                                //    {
                                //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                //        {
                                //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                //            {
                                //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                //                checkList[a].wqb99 = ee.NewTextValue;
                                //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                //            }

                                //        }

                                //    }
                                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                //    //{
                                //    //    //////Console.WriteLine("IMMMM~~~~");
                                //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                //    //    {
                                //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                //    //        foreach (var TempAnsList in c)
                                //    //        {
                                //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                //    //            //////Console.WriteLine("WHO~~" + who);
                                //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                //    //            {
                                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                //    //                {
                                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                //    //                    for (int d = 0; d < checkList.Count(); d++)
                                //    //                    {
                                //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                //    //                        {
                                //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                //    //                        }

                                //    //                    }

                                //    //                    //temp_j = TempAnsList.wqb01;
                                //    //                    //temp_value = TempAnsList.wqb99;
                                //    //                    //var check2 = new checkInfo
                                //    //                    //{
                                //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                //    //                    //    wqb99 = TempAnsList.wqb99

                                //    //                    //};

                                //    //                    //checkList.Add(check2);

                                //    //                    //MapView.AccDatabase.DeleteItem(b);
                                //    //                }
                                //    //            }


                                //    //        }
                                //    //    }



                                //    //}
                                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                //    //{
                                //    //    //////Console.WriteLine("IMMMM~~~~");

                                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                //    //    {
                                //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                //    //        foreach (var TempAnsList in a)
                                //    //        {
                                //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                //    //            {
                                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                //    //                {
                                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                //    //                    //temp_j = TempAnsList.wqb01;
                                //    //                    for (int c = 0; c < checkList.Count(); c++)
                                //    //                    {
                                //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                //    //                        {
                                //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                //    //                        }
                                //    //                    }
                                //    //                }
                                //    //            }

                                //    //        }
                                //    //    }


                                //    //}
                                //};
                                //}
                                check_box.CheckedChanged += async (s, e) =>
                                {
                                    //////Console.WriteLine("checkboxin2~~~");
                                    if (e.Value) // 如果選是，要跳出entry所以需要reset
                                    {
                                        ////////Console.WriteLine("IN~~~");
                                        //ischeck = true;
                                        ////////Console.WriteLine("evalue~~~ " + e.Value.ToString());
                                        //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                    //checkList2.RemoveAt(a);
                                                }
                                            }

                                        }
                                        ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                        //if (tmp_name_list.Contains(questionList.ClientName))
                                        //{
                                        //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                        //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                        //    //////Console.WriteLine("a~ " + total);
                                        //    tmp_name_list.Remove(questionList.ClientName);
                                        //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                        //    //////Console.WriteLine("b~ " + total2);
                                        //}

                                        //if (j == "是")
                                        //{
                                        //    ANS = 0;
                                        //    ANS2 = Convert.ToString(ANS);
                                        //}
                                        //else
                                        //{
                                        //    ANS = 1;
                                        //    ANS2 = Convert.ToString(ANS);
                                        //}
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            //////Console.WriteLine("j00~~ " + j);
                                            //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (j == i.qb03[d])
                                            {

                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                //////Console.WriteLine("j~~ " + j);
                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            //////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        // //////Console.WriteLine("cc~~~ " + p);
                                        //////Console.WriteLine("ANS2~~ " + ANS2);
                                        // 把問題選項存進資料庫
                                        ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                        //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                        //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                        ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                        ////////Console.WriteLine("j~~" + j);
                                        if (j == "是" || j == "已發")
                                        {
                                            color = "Red";
                                        }
                                        else
                                        {
                                            color = "Green";
                                        }
                                        //////Console.WriteLine("color~~~ " + color);
                                        QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                        ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                        var check = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            wqb01 = ANS2 // 答案

                                        };
                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = ANS2 // 答案

                                        };
                                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        checkList2.Add(check3); // for save
                                                                //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                        checkList.Add(check); // for check
                                                              //checkList2.Add(check3); // for save
                                                              ////////Console.WriteLine("CHECK" + checkList[0]);
                                        isReset = true;
                                        reset();

                                        // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                    }
                                    else // 選否則不用reset
                                    {
                                        //ischeck = false;
                                        //////Console.WriteLine("remove~~~~~");
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            if (checkList[a].qb_s_num == i.qb_s_num)
                                            {
                                                checkList.RemoveAt(a);
                                            }
                                        }
                                    }

                                    //foreach (var b in checkList)
                                    //{
                                    //    //////Console.WriteLine("HERE~~");
                                    //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                    //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                    //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                    //    //////Console.WriteLine("enrty : " + b.wqb99);
                                    //}
                                };

                                if (isReset == true || isDB == true)
                                {
                                    if (isRed == true && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                    {
                                        //////Console.WriteLine("JKL1~~~ " + j);
                                        if (j == "是" || j == "已發")
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Red,
                                                FontSize = 20
                                            };
                                            isRed = false;
                                        }

                                    }
                                    else if (isGreen == true && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                    {
                                        //////Console.WriteLine("JKL2~~~ " + j);
                                        if (j == "未發" || j == "否")
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Green,
                                                FontSize = 20
                                            };
                                            isGreen = false;
                                        }

                                    }
                                    else
                                    {
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }
                                }
                                else
                                {
                                    label_check = new Label // 選項
                                    {
                                        Text = j,
                                        TextColor = Color.Black,
                                        FontSize = 20
                                    };
                                }

                                var stack_check = new StackLayout // checkbox跟選項
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children = { check_box, label_check }
                                };

                                //var ques_all_check = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Horizontal,
                                //    Children = { stack_check, stack }
                                //};

                                //var stack_enrty = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Vertical,
                                //    Children = { stack_check, entny }
                                //};
                                stack_ques.Children.Add(stack_check);
                                //count = count + 1;

                                //var final_stack = new StackLayout
                                //{
                                //    Orientation = StackOrientation.Horizontal,
                                //    Children = { stack_ques, label_que_name }
                                //};
                            }
                        } // 星期 1 ~ 3 只有已發
                        else
                        {
                            foreach (var j in i.qb03)
                            {

                                if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期五") == true)
                                {
                                    ////////Console.WriteLine("friday_in~~~ ");
                                    //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                    //{
                                    //count = 0;
                                    ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                                    ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                                    ////Console.WriteLine("order~~~ " + i.qb_order);
                                    ////Console.WriteLine("result_num~~~ " + result_num);
                                    TFcount = TFcount + 1;
                                    var temp_j = "";
                                    var temp_value = "";
                                    //var temp_j_map = "";
                                    //var temp_value_map = "";
                                    ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                                    if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                    {
                                        ////////Console.WriteLine("first~~ ");
                                        ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        var _wqhsnum = questionList.wqh_s_num;
                                        temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                        ////////Console.WriteLine("tempj~~ " + temp_j);
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            ////////Console.WriteLine("j00~~ " + j);
                                            ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (temp_j == i.qb03[d])
                                            {

                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                ////////Console.WriteLine("j~~ " + j);
                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                ////////Console.WriteLine("jj~~ " + temp_j);
                                                ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                                        //if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                        //{
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                            AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                            ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //}

                                        // ////////Console.WriteLine("cc~~~ " + p);
                                        ////////Console.WriteLine("ANS2~~ " + ANS2);
                                        //var ques = questionList.wqh_s_num + i.qb_order;
                                        //////////Console.WriteLine("ques!!! " + ques);
                                        //for(int e = 0; e < checkList2.Count(); e++)
                                        //{
                                        //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                        //    ////////Console.WriteLine("check!!! " + check);
                                        //    if (check != ques)
                                        //    {
                                        //        ////////Console.WriteLine("checkin!!! " + check);
                                        //        ////////Console.WriteLine("quesin!!! " + ques);
                                        //        var check3 = new checkInfo
                                        //        {
                                        //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //            qb_order = i.qb_order,
                                        //            wqb01 = ANS2 // 答案

                                        //        };
                                        //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        //        checkList2.Add(check3); // for save
                                        //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //    }
                                        //}
                                        ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                        //qb03_count = 0;
                                        //foreach (var y in i.qb03)
                                        //{
                                        //    ////////Console.WriteLine("yAA~~~ " + y);
                                        //    if (temp_j == y)
                                        //    {
                                        //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                        //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                        //        p = Convert.ToString(qb03_count);
                                        //    }
                                        //    qb03_count = qb03_count + 1;

                                        //}
                                        //////////Console.WriteLine("cc~~~ " + p);
                                        ////temp_value = TempAnsList.wqb99; // 之前的entry
                                        //var check2 = new checkInfo
                                        //{
                                        //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    wqb01 = p,// 答案
                                        //              //wqb99 = TempAnsList.wqb99

                                        //};
                                    }
                                    // 跑選是的reset把checkList抓回來判斷
                                    ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                    for (int a = 0; a < checkList.Count(); a++)
                                    {
                                        ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                        ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                        //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        {
                                            //////////Console.WriteLine("IMMMM222~~~~");
                                            ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                            {
                                                ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                //foreach (var w in i.qb03)
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                    ////////Console.WriteLine("w00~~~ " + d.ToString());
                                                    if (checkList[a].wqb01 == d.ToString())
                                                    {

                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        ////////Console.WriteLine("j~~ " + j);
                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        temp_j = i.qb03[d]; // 答案
                                                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                                    }

                                                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // ////////Console.WriteLine("cc~~~ " + p);
                                                ////////Console.WriteLine("ANS2~~ " + ANS2);

                                                //temp_value = checkList[a].wqb99; // entry
                                            }
                                        }
                                    }
                                    ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                                    ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                                    ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                                    // ------------------------------------------

                                    //////////Console.WriteLine("")
                                    //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //~~~~~~~~~~~~~~~~~~~~~~~~
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                    ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                                    ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                                    ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                                    ////////////Console.WriteLine("ID" + TempAnsList.ID);
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    //checkList.Add(check2);
                                    //checkList2.Add(check2);
                                    //~~~~~~~~~~~~~~~~~~~~`
                                    ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                                    ////////Console.WriteLine("j~~ " + j);

                                    bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                 //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                    if (ischeck == true)
                                    {
                                        //if (j == "是" || j == "已發")
                                        //{
                                        //    isRed = true;
                                        //}
                                        //else
                                        //{
                                        //    isGreen = true;
                                        //}

                                    }
                                    ////////Console.WriteLine("j1~~~ " + j);
                                    ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                                    ////////Console.WriteLine("isRed1~~~ " + isRed);
                                    ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                                    ////////Console.WriteLine("TFcount~~~" + TFcount);
                                    //if (j == "是" || j == "已發")
                                    ////////Console.WriteLine("name~~ " + questionList.ClientName);
                                    ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (TFcount == 1)
                                    {
                                        ////////Console.WriteLine("Red~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Green
                                        };
                                    }
                                    else
                                    {
                                        ////////Console.WriteLine("Green~~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Red
                                        };
                                    }



                                    //if (j == "是")
                                    //{
                                    //entny = new Entry // 產生Entry
                                    //{
                                    //    Placeholder = "請說明",
                                    //    Text = temp_value,
                                    //    IsVisible = isEntry,
                                    //    IsEnabled = isEntry


                                    //};



                                    //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                    //{
                                    //    for (int a = 0; a < checkList.Count(); a++)
                                    //    {
                                    //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                    //        {
                                    //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                    //            {
                                    //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                    //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                    //                checkList[a].wqb99 = ee.NewTextValue;
                                    //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                                    //            }

                                    //        }

                                    //    }
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");
                                    //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                    //    //        foreach (var TempAnsList in c)
                                    //    //        {
                                    //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                    //    //            ////////Console.WriteLine("WHO~~" + who);
                                    //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    for (int d = 0; d < checkList.Count(); d++)
                                    //    //                    {
                                    //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                    //    //                        }

                                    //    //                    }

                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    //temp_value = TempAnsList.wqb99;
                                    //    //                    //var check2 = new checkInfo
                                    //    //                    //{
                                    //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                    //    //                    //    wqb99 = TempAnsList.wqb99

                                    //    //                    //};

                                    //    //                    //checkList.Add(check2);

                                    //    //                    //MapView.AccDatabase.DeleteItem(b);
                                    //    //                }
                                    //    //            }


                                    //    //        }
                                    //    //    }



                                    //    //}
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");

                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                    //    //        foreach (var TempAnsList in a)
                                    //    //        {
                                    //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    for (int c = 0; c < checkList.Count(); c++)
                                    //    //                    {
                                    //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                    //    //                        }
                                    //    //                    }
                                    //    //                }
                                    //    //            }

                                    //    //        }
                                    //    //    }


                                    //    //}
                                    //};
                                    //}
                                    check_box.CheckedChanged += async (s, e) =>
                                    {
                                        ////////Console.WriteLine("checkboxin1~~~");
                                        if (e.Value) // 如果選是，要跳出entry所以需要reset
                                        {
                                            //////////Console.WriteLine("IN~~~");
                                            //ischeck = true;
                                            //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                        //checkList2.RemoveAt(a);
                                                    }
                                                }

                                            }
                                            //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //if (tmp_name_list.Contains(questionList.ClientName))
                                            //{
                                            //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                            //    ////////Console.WriteLine("a~ " + total);
                                            //    tmp_name_list.Remove(questionList.ClientName);
                                            //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                            //    ////////Console.WriteLine("b~ " + total2);
                                            //}
                                            ////////Console.WriteLine("jjj~~~ " + j);
                                            if (j == "未發")
                                            {
                                                //IsChoose = true;
                                                CheckboxList[questionList.ClientName] = true;
                                                ChooseSaveToDB(questionList.ClientName, true);
                                                ////////Console.WriteLine("LLL~~~ " + IsChoose);
                                                ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                                            }
                                            //if (j == "是")
                                            //{
                                            //    ANS = 0;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else
                                            //{
                                            //    ANS = 1;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            ////////Console.WriteLine("j~~~ " + j);
                                            //qb03_count = 0;
                                            //foreach(var w in i.qb03)
                                            //{
                                            //    if(j == w)
                                            //    {
                                            //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            //        ////////Console.WriteLine("j~~ " + j);
                                            //        ////////Console.WriteLine("w~~~ " + w);

                                            //        ANS2 = Convert.ToString(qb03_count);
                                            //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            //    }
                                            //    qb03_count = qb03_count + 1;
                                            //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            //}
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                ////////Console.WriteLine("j00~~ " + j);
                                                ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (j == i.qb03[d])
                                                {

                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    ////////Console.WriteLine("j~~ " + j);
                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                                                    ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // ////////Console.WriteLine("cc~~~ " + p);
                                            ////////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var check3 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    qb_order = i.qb_order,
                                            //    wqb01 = ANS2 // 答案

                                            //};
                                            //////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //checkList2.Add(check3); // for save
                                            ////////Console.WriteLine("checkList2Add1~~~ ");

                                            //foreach (var k in checkList2)
                                            //{
                                            ////////Console.WriteLine("a~ " + k.wqh_s_num);
                                            ////////Console.WriteLine("b~ " + k.wqb01);
                                            //}
                                            ////////Console.WriteLine("count2~~ " + checkList2.Count());
                                            // 把問題選項存進資料庫
                                            //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                            ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                            ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                            //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                            //////////Console.WriteLine("j~~" + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                color = "Red";
                                            }
                                            else
                                            {
                                                color = "Green";
                                            }
                                            ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                            ////////Console.WriteLine("color~~~ " + color);
                                            ////////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                                            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);

                                            ////////Console.WriteLine("j_HERE~~~ " + j);
                                            var check = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save

                                            ////////Console.WriteLine("wqb01~~ " + ANS2);
                                            ////////Console.WriteLine("j_HERE2~~~ " + j);
                                            ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                            checkList.Add(check); // for check

                                            //////////Console.WriteLine("CHECK" + checkList[0]);
                                            ////////Console.WriteLine("checkList2ADD~~ ");

                                            //if (j == "未發")
                                            //{
                                            //    ////Console.WriteLine("reset~friday~~ ");
                                            //    reset();
                                            //}
                                            reset();
                                            isReset = true;
                                            ////Console.WriteLine("ISREST~~~ " + isReset);
                                            // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                        }
                                        else //
                                        {
                                            //ischeck = false;
                                            ////////Console.WriteLine("remove~~~~~");
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                }
                                            }
                                        }

                                        //foreach (var b in checkList)
                                        //{
                                        //    ////////Console.WriteLine("HERE~~");
                                        //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                        //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                        //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                                        //    ////////Console.WriteLine("enrty : " + b.wqb99);
                                        //}
                                    };
                                    ////Console.WriteLine("isreset~~~ ");
                                    ////Console.WriteLine("isRest~~ " + isReset);
                                    if (isReset == true || isDB == true)
                                    {
                                        if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            ////Console.WriteLine("A~~~ " + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                ////Console.WriteLine("A-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Red,
                                                    FontSize = 20
                                                };
                                                isRed = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("A-2~~~ ");
                                                ////////Console.WriteLine("JKL1+1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            ////Console.WriteLine("B~~~~ " + j);
                                            if (j == "已發" || j == "否")
                                            {
                                                ////Console.WriteLine("B-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Green,
                                                    FontSize = 20
                                                };
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("B-2~~~ ");
                                                ////////Console.WriteLine("JKL2-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            ////Console.WriteLine("C~~~ " + j);
                                            ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                                            ////Console.WriteLine("j~~~ " + j);
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };

                                        }
                                    }
                                    else
                                    {
                                        ////Console.WriteLine("D~~~ ");
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }


                                    var stack_check = new StackLayout // checkbox跟選項
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children = { check_box, label_check }
                                    };

                                    //var ques_all_check = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_check, stack }
                                    //};

                                    //var stack_enrty = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Vertical,
                                    //    Children = { stack_check, entny }
                                    //};
                                    stack_ques.Children.Add(stack_check);

                                    //count = count + 1;

                                    //var final_stack = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_ques, label_que_name }
                                    //};
                                    //}
                                }
                                else if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期四") == true)
                                {
                                    //Console.WriteLine("thusday_in~~~ ");
                                    //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                    //{
                                    //count = 0;
                                    ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                                    ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                                    ////Console.WriteLine("order~~~ " + i.qb_order);
                                    ////Console.WriteLine("result_num~~~ " + result_num);
                                    TFcount = TFcount + 1;
                                    var temp_j = "";
                                    var temp_value = "";
                                    //var temp_j_map = "";
                                    //var temp_value_map = "";
                                    ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                                    if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                    {
                                        ////////Console.WriteLine("first~~ ");
                                        ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        var _wqhsnum = questionList.wqh_s_num;
                                        temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                        ////////Console.WriteLine("tempj~~ " + temp_j);
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            ////////Console.WriteLine("j00~~ " + j);
                                            ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (temp_j == i.qb03[d])
                                            {

                                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                //Console.WriteLine("j~~ " + j);
                                                //Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                ////////Console.WriteLine("jj~~ " + temp_j);
                                                ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                                        //if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                        //{
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                            AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                            ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //}

                                        // ////////Console.WriteLine("cc~~~ " + p);
                                        ////////Console.WriteLine("ANS2~~ " + ANS2);
                                        //var ques = questionList.wqh_s_num + i.qb_order;
                                        //////////Console.WriteLine("ques!!! " + ques);
                                        //for(int e = 0; e < checkList2.Count(); e++)
                                        //{
                                        //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                        //    ////////Console.WriteLine("check!!! " + check);
                                        //    if (check != ques)
                                        //    {
                                        //        ////////Console.WriteLine("checkin!!! " + check);
                                        //        ////////Console.WriteLine("quesin!!! " + ques);
                                        //        var check3 = new checkInfo
                                        //        {
                                        //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //            qb_order = i.qb_order,
                                        //            wqb01 = ANS2 // 答案

                                        //        };
                                        //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        //        checkList2.Add(check3); // for save
                                        //        ////////Console.WriteLine("checkList2Add2~~~ ");
                                        //    }
                                        //}
                                        ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                        //qb03_count = 0;
                                        //foreach (var y in i.qb03)
                                        //{
                                        //    ////////Console.WriteLine("yAA~~~ " + y);
                                        //    if (temp_j == y)
                                        //    {
                                        //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                        //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                        //        p = Convert.ToString(qb03_count);
                                        //    }
                                        //    qb03_count = qb03_count + 1;

                                        //}
                                        //////////Console.WriteLine("cc~~~ " + p);
                                        ////temp_value = TempAnsList.wqb99; // 之前的entry
                                        //var check2 = new checkInfo
                                        //{
                                        //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    wqb01 = p,// 答案
                                        //              //wqb99 = TempAnsList.wqb99

                                        //};
                                    }
                                    // 跑選是的reset把checkList抓回來判斷
                                    ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                    for (int a = 0; a < checkList.Count(); a++)
                                    {
                                        ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                        ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                        //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        {
                                            //////////Console.WriteLine("IMMMM222~~~~");
                                            ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                            {
                                                ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                //foreach (var w in i.qb03)
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                    ////////Console.WriteLine("w00~~~ " + d.ToString());
                                                    if (checkList[a].wqb01 == d.ToString())
                                                    {

                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        ////////Console.WriteLine("j~~ " + j);
                                                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        temp_j = i.qb03[d]; // 答案
                                                                            ////////Console.WriteLine("jj~~ " + temp_j);
                                                    }

                                                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // ////////Console.WriteLine("cc~~~ " + p);
                                                ////////Console.WriteLine("ANS2~~ " + ANS2);

                                                //temp_value = checkList[a].wqb99; // entry
                                            }
                                        }
                                    }
                                    ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                                    ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                                    ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                                    ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                                    ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                                    // ------------------------------------------

                                    //////////Console.WriteLine("")
                                    //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //~~~~~~~~~~~~~~~~~~~~~~~~
                                    //qb03_count = 0;
                                    //foreach (var y in i.qb03)
                                    //{
                                    //    ////////Console.WriteLine("yAA~~~ " + y);
                                    //    if (temp_j == y)
                                    //    {
                                    //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                                    //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                                    //        p = Convert.ToString(qb03_count);
                                    //    }
                                    //    qb03_count = qb03_count + 1;

                                    //}
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                                    //var check2 = new checkInfo
                                    //{
                                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    wqb01 = p,// 答案
                                    //              //wqb99 = TempAnsList.wqb99

                                    //};
                                    ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                                    ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                                    ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                                    ////////////Console.WriteLine("ID" + TempAnsList.ID);
                                    //////////Console.WriteLine("cc~~~ " + p);
                                    //checkList.Add(check2);
                                    //checkList2.Add(check2);
                                    //~~~~~~~~~~~~~~~~~~~~`
                                    ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                                    ////////Console.WriteLine("j~~ " + j);

                                    bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                 //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                    if (ischeck == true)
                                    {
                                        //if (j == "是" || j == "已發")
                                        //{
                                        //    isRed = true;
                                        //}
                                        //else
                                        //{
                                        //    isGreen = true;
                                        //}

                                    }
                                    ////////Console.WriteLine("j1~~~ " + j);
                                    ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                                    ////////Console.WriteLine("isRed1~~~ " + isRed);
                                    ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                                    ////////Console.WriteLine("TFcount~~~" + TFcount);
                                    //if (j == "是" || j == "已發")
                                    ////////Console.WriteLine("name~~ " + questionList.ClientName);
                                    ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (TFcount == 1)
                                    {
                                        ////////Console.WriteLine("Red~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Green
                                        };
                                    }
                                    else
                                    {
                                        ////////Console.WriteLine("Green~~~ ");
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Red
                                        };
                                    }



                                    //if (j == "是")
                                    //{
                                    //entny = new Entry // 產生Entry
                                    //{
                                    //    Placeholder = "請說明",
                                    //    Text = temp_value,
                                    //    IsVisible = isEntry,
                                    //    IsEnabled = isEntry


                                    //};



                                    //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                    //{
                                    //    for (int a = 0; a < checkList.Count(); a++)
                                    //    {
                                    //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                    //        {
                                    //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                    //            {
                                    //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                    //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                    //                checkList[a].wqb99 = ee.NewTextValue;
                                    //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                                    //            }

                                    //        }

                                    //    }
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");
                                    //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                    //    //        foreach (var TempAnsList in c)
                                    //    //        {
                                    //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                    //    //            ////////Console.WriteLine("WHO~~" + who);
                                    //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    for (int d = 0; d < checkList.Count(); d++)
                                    //    //                    {
                                    //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                    //    //                        }

                                    //    //                    }

                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    //temp_value = TempAnsList.wqb99;
                                    //    //                    //var check2 = new checkInfo
                                    //    //                    //{
                                    //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                    //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                    //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                    //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                    //    //                    //    wqb99 = TempAnsList.wqb99

                                    //    //                    //};

                                    //    //                    //checkList.Add(check2);

                                    //    //                    //MapView.AccDatabase.DeleteItem(b);
                                    //    //                }
                                    //    //            }


                                    //    //        }
                                    //    //    }



                                    //    //}
                                    //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                    //    //{
                                    //    //    ////////Console.WriteLine("IMMMM~~~~");

                                    //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                    //    //    {
                                    //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                    //    //        foreach (var TempAnsList in a)
                                    //    //        {
                                    //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                    //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                    //    //            {
                                    //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                    //    //                {
                                    //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                    //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                    //    //                    //temp_j = TempAnsList.wqb01;
                                    //    //                    for (int c = 0; c < checkList.Count(); c++)
                                    //    //                    {
                                    //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                    //    //                        {
                                    //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                    //    //                        }
                                    //    //                    }
                                    //    //                }
                                    //    //            }

                                    //    //        }
                                    //    //    }


                                    //    //}
                                    //};
                                    //}
                                    check_box.CheckedChanged += async (s, e) =>
                                    {
                                        ////////Console.WriteLine("checkboxin1~~~");
                                        if (e.Value) // 如果選是，要跳出entry所以需要reset
                                        {
                                            //////////Console.WriteLine("IN~~~");
                                            //ischeck = true;
                                            //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                        //checkList2.RemoveAt(a);
                                                    }
                                                }

                                            }
                                            //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //if (tmp_name_list.Contains(questionList.ClientName))
                                            //{
                                            //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                            //    ////////Console.WriteLine("a~ " + total);
                                            //    tmp_name_list.Remove(questionList.ClientName);
                                            //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                            //    ////////Console.WriteLine("b~ " + total2);
                                            //}
                                            ////////Console.WriteLine("jjj~~~ " + j);
                                            if (j == "未發")
                                            {
                                                //IsChoose = true;
                                                CheckboxList[questionList.ClientName ] = true;
                                                ChooseSaveToDB(questionList.ClientName, true);
                                                ////////Console.WriteLine("LLL~~~ " + IsChoose);
                                                ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                                            }
                                            //if (j == "是")
                                            //{
                                            //    ANS = 0;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else
                                            //{
                                            //    ANS = 1;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            ////////Console.WriteLine("j~~~ " + j);
                                            //qb03_count = 0;
                                            //foreach(var w in i.qb03)
                                            //{
                                            //    if(j == w)
                                            //    {
                                            //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                            //        ////////Console.WriteLine("j~~ " + j);
                                            //        ////////Console.WriteLine("w~~~ " + w);

                                            //        ANS2 = Convert.ToString(qb03_count);
                                            //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            //    }
                                            //    qb03_count = qb03_count + 1;
                                            //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            //}
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                ////////Console.WriteLine("j00~~ " + j);
                                                ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (j == i.qb03[d])
                                                {

                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    ////////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    ////////Console.WriteLine("j~~ " + j);
                                                    ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                                                    ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                ////////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // ////////Console.WriteLine("cc~~~ " + p);
                                            ////////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var check3 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    qb_order = i.qb_order,
                                            //    wqb01 = ANS2 // 答案

                                            //};
                                            //////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //checkList2.Add(check3); // for save
                                            ////////Console.WriteLine("checkList2Add1~~~ ");

                                            //foreach (var k in checkList2)
                                            //{
                                            ////////Console.WriteLine("a~ " + k.wqh_s_num);
                                            ////////Console.WriteLine("b~ " + k.wqb01);
                                            //}
                                            ////////Console.WriteLine("count2~~ " + checkList2.Count());
                                            // 把問題選項存進資料庫
                                            //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                            ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                            ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                            //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                            //////////Console.WriteLine("j~~" + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                color = "Red";
                                            }
                                            else
                                            {
                                                color = "Green";
                                            }
                                            ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                            //Console.WriteLine("color~~~ " + color);
                                            //Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                                            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);

                                            //Console.WriteLine("j_HERE~~~ " + j);
                                            var check = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save

                                            ////////Console.WriteLine("wqb01~~ " + ANS2);
                                            ////////Console.WriteLine("j_HERE2~~~ " + j);
                                            ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                            checkList.Add(check); // for check

                                            //////////Console.WriteLine("CHECK" + checkList[0]);
                                            ////////Console.WriteLine("checkList2ADD~~ ");

                                            //if (j == "未發")
                                            //{
                                            //    ////Console.WriteLine("reset~friday~~ ");
                                            //    reset();
                                            //}
                                            reset();
                                            isReset = true;
                                            ////Console.WriteLine("ISREST~~~ " + isReset);
                                            // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                        }
                                        else //
                                        {
                                            //ischeck = false;
                                            ////////Console.WriteLine("remove~~~~~");
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                }
                                            }
                                        }

                                        //foreach (var b in checkList)
                                        //{
                                        //    ////////Console.WriteLine("HERE~~");
                                        //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                        //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                        //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                                        //    ////////Console.WriteLine("enrty : " + b.wqb99);
                                        //}
                                    };
                                    ////Console.WriteLine("isreset~~~ ");
                                    ////Console.WriteLine("isRest~~ " + isReset);
                                    //Console.WriteLine("bingoname22~~~ " + questionList.wqh_s_num);
                                    //Console.WriteLine("qborder22~~ " + i.qb_order);
                                    //Console.WriteLine("color22~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                                    if (isReset == true || isDB == true)
                                    {
                                        if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //Console.WriteLine("A~~~ " + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                ////Console.WriteLine("A-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Red,
                                                    FontSize = 20
                                                };
                                                isRed = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("A-2~~~ ");
                                                ////////Console.WriteLine("JKL1+1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //Console.WriteLine("B~~~~ " + j);
                                            if (j == "已發" || j == "否")
                                            {
                                                //Console.WriteLine("B-1~~~ ");
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Green,
                                                    FontSize = 20
                                                };
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                ////Console.WriteLine("B-2~~~ ");
                                                ////////Console.WriteLine("JKL2-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            ////Console.WriteLine("C~~~ " + j);
                                            ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                                            ////Console.WriteLine("j~~~ " + j);
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };

                                        }
                                    }
                                    else
                                    {
                                        ////Console.WriteLine("D~~~ ");
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }


                                    var stack_check = new StackLayout // checkbox跟選項
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children = { check_box, label_check }
                                    };

                                    //var ques_all_check = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_check, stack }
                                    //};

                                    //var stack_enrty = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Vertical,
                                    //    Children = { stack_check, entny }
                                    //};
                                    stack_ques.Children.Add(stack_check);

                                    //count = count + 1;

                                    //var final_stack = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_ques, label_que_name }
                                    //};
                                    //}
                                }
                                else
                                {
                                    if (i.qb_order == "1")
                                    {
                                        //Console.WriteLine("qborder1~~in~~~");
                                        //Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                                        //Console.WriteLine("name~~ " + questionList.ClientName);
                                        //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                        //{
                                        //count = 0;
                                        ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                                        ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                                        ////Console.WriteLine("order~~~ " + i.qb_order);
                                        ////Console.WriteLine("result_num~~~ " + result_num);
                                        TFcount = TFcount + 1;
                                        var temp_j = "";
                                        var temp_value = "";
                                        //var temp_j_map = "";
                                        //var temp_value_map = "";

                                        // 跑選是的reset把checkList抓回來判斷
                                        if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                        {
                                            //////Console.WriteLine("third~~ ");
                                            //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            var _wqhsnum = questionList.wqh_s_num;
                                            temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                            //////Console.WriteLine("tempj~~ " + temp_j);
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("j00~~ " + j);
                                                //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (temp_j == i.qb03[d])
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    //////Console.WriteLine("jj~~ " + temp_j);
                                                    //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                                                    //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                                                                    //{
                                                                    //    //////Console.WriteLine("ans2~~~ " + ANS2);
                                                                    //    var check3 = new checkInfo
                                                                    //    {
                                                                    //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                                    //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                                    //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                                    //        qb_order = i.qb_order,
                                                                    //        wqb01 = ANS2 // 答案

                                            //    };
                                            //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //    checkList2.Add(check3); // for save
                                            //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                                            //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                                            //    //////Console.WriteLine("checkList2Add99~~~ ");
                                            //}

                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var ques = questionList.wqh_s_num + i.qb_order;
                                            ////////Console.WriteLine("ques!!! " + ques);
                                            //for(int e = 0; e < checkList2.Count(); e++)
                                            //{
                                            //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                            //    //////Console.WriteLine("check!!! " + check);
                                            //    if (check != ques)
                                            //    {
                                            //        //////Console.WriteLine("checkin!!! " + check);
                                            //        //////Console.WriteLine("quesin!!! " + ques);
                                            //        var check3 = new checkInfo
                                            //        {
                                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //            qb_order = i.qb_order,
                                            //            wqb01 = ANS2 // 答案

                                            //        };
                                            //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //        checkList2.Add(check3); // for save
                                            //        //////Console.WriteLine("checkList2Add2~~~ ");
                                            //    }
                                            //}
                                            //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                            //qb03_count = 0;
                                            //foreach (var y in i.qb03)
                                            //{
                                            //    //////Console.WriteLine("yAA~~~ " + y);
                                            //    if (temp_j == y)
                                            //    {
                                            //        //////Console.WriteLine("temp_j~~ " + temp_j);
                                            //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                                            //        p = Convert.ToString(qb03_count);
                                            //    }
                                            //    qb03_count = qb03_count + 1;

                                            //}
                                            ////////Console.WriteLine("cc~~~ " + p);
                                            ////temp_value = TempAnsList.wqb99; // 之前的entry
                                            //var check2 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    wqb01 = p,// 答案
                                            //              //wqb99 = TempAnsList.wqb99

                                            //};
                                        }
                                        // 跑選是的reset把checkList抓回來判斷
                                        //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                            //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                            {
                                                ////////Console.WriteLine("IMMMM222~~~~");
                                                //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                                {
                                                    //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                    //foreach (var w in i.qb03)
                                                    for (int d = 0; d < i.qb03.Count(); d++)
                                                    {
                                                        //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                        //////Console.WriteLine("w00~~~ " + d.ToString());
                                                        if (checkList[a].wqb01 == d.ToString())
                                                        {

                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                            //////Console.WriteLine("j~~ " + j);
                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //ANS2 = Convert.ToString(qb03_count);
                                                            temp_j = i.qb03[d]; // 答案
                                                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                        }

                                                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                    }
                                                    // //////Console.WriteLine("cc~~~ " + p);
                                                    //////Console.WriteLine("ANS2~~ " + ANS2);

                                                    //temp_value = checkList[a].wqb99; // entry
                                                }
                                            }
                                        }



                                        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                     //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                        if (ischeck == true)
                                        {
                                            if (j == "是" || j == "已發")
                                            {
                                                isRed = true;
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                isGreen = true;
                                                isRed = false;
                                            }
                                        }
                                        //////Console.WriteLine("j3~~~ " + j);
                                        //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                                        //////Console.WriteLine("isRed3~~~ " + isRed);
                                        //////Console.WriteLine("isGreen3~~~ " + isGreen);

                                        //////Console.WriteLine("TFcount~~~" + TFcount);
                                        if (TFcount == 1)
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Red,

                                            };
                                        }
                                        else
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Green
                                            };
                                        }



                                        //if (j == "是")
                                        //{
                                        //entny = new Entry // 產生Entry
                                        //{
                                        //    Placeholder = "請說明",
                                        //    Text = temp_value,
                                        //    IsVisible = isEntry,
                                        //    IsEnabled = isEntry


                                        //};



                                        //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                        //{
                                        //    for (int a = 0; a < checkList.Count(); a++)
                                        //    {
                                        //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                        //        {
                                        //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                        //            {
                                        //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                        //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                        //                checkList[a].wqb99 = ee.NewTextValue;
                                        //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                        //            }

                                        //        }

                                        //    }
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");
                                        //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                        //    //        foreach (var TempAnsList in c)
                                        //    //        {
                                        //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                        //    //            //////Console.WriteLine("WHO~~" + who);
                                        //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    for (int d = 0; d < checkList.Count(); d++)
                                        //    //                    {
                                        //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                        //    //                        }

                                        //    //                    }

                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    //temp_value = TempAnsList.wqb99;
                                        //    //                    //var check2 = new checkInfo
                                        //    //                    //{
                                        //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                        //    //                    //    wqb99 = TempAnsList.wqb99

                                        //    //                    //};

                                        //    //                    //checkList.Add(check2);

                                        //    //                    //MapView.AccDatabase.DeleteItem(b);
                                        //    //                }
                                        //    //            }


                                        //    //        }
                                        //    //    }



                                        //    //}
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");

                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                        //    //        foreach (var TempAnsList in a)
                                        //    //        {
                                        //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    for (int c = 0; c < checkList.Count(); c++)
                                        //    //                    {
                                        //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                        //    //                        }
                                        //    //                    }
                                        //    //                }
                                        //    //            }

                                        //    //        }
                                        //    //    }


                                        //    //}
                                        //};
                                        //}
                                        check_box.CheckedChanged += async (s, e) =>
                                        {
                                            //////Console.WriteLine("checkboxin3~~~");
                                            if (e.Value) // 如果選是，要跳出entry所以需要reset
                                            {
                                                ////////Console.WriteLine("IN~~~");
                                                //ischeck = true;
                                                //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                    {
                                                        if (checkList[a].qb_s_num == i.qb_s_num)
                                                        {
                                                            checkList.RemoveAt(a);
                                                            //checkList2.RemoveAt(a);
                                                        }
                                                    }

                                                }
                                                ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //if (tmp_name_list.Contains(questionList.ClientName))
                                                //{
                                                //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                                //    //////Console.WriteLine("a~ " + total);
                                                //    tmp_name_list.Remove(questionList.ClientName);
                                                //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                                //    //////Console.WriteLine("b~ " + total2);
                                                //}
                                                //////Console.WriteLine("jjj~~~ " + j);
                                                if (j == "未發")
                                                {
                                                    //IsChoose = true;
                                                    CheckboxList[questionList.ClientName ] = true;
                                                    ChooseSaveToDB(questionList.ClientName, true);
                                                    //////Console.WriteLine("LLL~~~ " + IsChoose);
                                                }
                                                //if (j == "是" || j == "已發")
                                                //{
                                                //    ANS = 0;
                                                //    ANS2 = Convert.ToString(ANS);


                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS2 = Convert.ToString(ANS);

                                                //}
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    //Console.WriteLine("j00~~ " + j);
                                                    //Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                    if (j == i.qb03[d])
                                                    {

                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                        //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        //////Console.WriteLine("j~~ " + j);
                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        ANS2 = d.ToString();
                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                        //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                    }

                                                    //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS2~~ " + ANS2);
                                                //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS21~~~ " + ANS2);
                                                // 把問題選項存進資料庫
                                                ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                                //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                                //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                                ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                                //Console.WriteLine("j~~" + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    //Console.WriteLine("G_in~~~ ");
                                                    color = "Green";
                                                }
                                                else
                                                {
                                                    //Console.WriteLine("R_in~~~ ");
                                                    color = "Red";
                                                }
                                                //////Console.WriteLine("color~~~ " + color);
                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                                ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    wqb01 = ANS2 // 答案

                                                };
                                                checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    qb_order = i.qb_order,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                                checkList2.Add(check3); // for save
                                                                        //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList.Add(check); // for check
                                                                      //checkList2.Add(check3); // for save
                                                                      ////////Console.WriteLine("CHECK" + checkList[0]);
                                                ///
                                                reset();
                                                //isReset = true;
                                                //if (j == "未發")
                                                //{
                                                //    reset();
                                                //}


                                                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                            }
                                            else // 選否則不用reset
                                            {
                                                //ischeck = false;
                                                //////Console.WriteLine("remove~~~~~");
                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                    }
                                                }
                                            }

                                            //foreach (var b in checkList)
                                            //{
                                            //    //////Console.WriteLine("HERE~~");
                                            //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                            //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                            //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                            //    //////Console.WriteLine("enrty : " + b.wqb99);
                                            //}
                                        };
                                        //////Console.WriteLine("isReset~~~~ " + isReset);
                                        //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                                        //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        //////Console.WriteLine("WWWRRR_j~~~ " + j);
                                        ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        if (isReset == true || isDB == true)
                                        {
                                            if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //Console.WriteLine("JKL1~~~ " + j);
                                                if (j == "是")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Green,
                                                        //Margin = new Thickness(-10,0,0,0),
                                                        FontSize = 20
                                                    };
                                                    isRed = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL1-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL2~~~ " + j);
                                                if (j == "否")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Red,
                                                        //Margin = new Thickness(-10, 0, 0, 0),
                                                        FontSize = 20
                                                    };
                                                    isGreen = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL2-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL3~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };
                                        }


                                        var stack_check = new StackLayout // checkbox跟選項
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Children = { check_box, label_check }
                                        };

                                        //var ques_all_check = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_check, stack }
                                        //};

                                        //var stack_enrty = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Vertical,
                                        //    Children = { stack_check, entny }
                                        //};
                                        stack_ques.Children.Add(stack_check);
                                        //count = count + 1;

                                        //var final_stack = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_ques, label_que_name }
                                        //};
                                        //}
                                    }
                                    else
                                    {
                                        //Console.WriteLine("qborder_2_in~~~ ");
                                        //Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                                        //Console.WriteLine("name~~ " + questionList.ClientName);
                                        //////Console.WriteLine("CVB~~~~");
                                        //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                        //{
                                        //count = 0;
                                        ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                                        ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                                        ////Console.WriteLine("order~~~ " + i.qb_order);
                                        ////Console.WriteLine("result_num~~~ " + result_num);
                                        TFcount = TFcount + 1;
                                        var temp_j = "";
                                        var temp_value = "";
                                        //var temp_j_map = "";
                                        //var temp_value_map = "";

                                        // 跑選是的reset把checkList抓回來判斷
                                        if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                        {
                                            //////Console.WriteLine("third~~ ");
                                            //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            var _wqhsnum = questionList.wqh_s_num;
                                            temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                            //////Console.WriteLine("tempj~~ " + temp_j);
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("j00~~ " + j);
                                                //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (temp_j == i.qb03[d])
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    //////Console.WriteLine("jj~~ " + temp_j);
                                                    //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                            //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                            //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                                                    //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                                                                    //{
                                                                    //    //////Console.WriteLine("ans2~~~ " + ANS2);
                                                                    //    var check3 = new checkInfo
                                                                    //    {
                                                                    //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                                    //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                                    //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                                    //        qb_order = i.qb_order,
                                                                    //        wqb01 = ANS2 // 答案

                                            //    };
                                            //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //    checkList2.Add(check3); // for save
                                            //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                                            //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                                            //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                                            //    //////Console.WriteLine("checkList2Add99~~~ ");
                                            //}

                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);
                                            //var ques = questionList.wqh_s_num + i.qb_order;
                                            ////////Console.WriteLine("ques!!! " + ques);
                                            //for(int e = 0; e < checkList2.Count(); e++)
                                            //{
                                            //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                            //    //////Console.WriteLine("check!!! " + check);
                                            //    if (check != ques)
                                            //    {
                                            //        //////Console.WriteLine("checkin!!! " + check);
                                            //        //////Console.WriteLine("quesin!!! " + ques);
                                            //        var check3 = new checkInfo
                                            //        {
                                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //            qb_order = i.qb_order,
                                            //            wqb01 = ANS2 // 答案

                                            //        };
                                            //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                                            //        checkList2.Add(check3); // for save
                                            //        //////Console.WriteLine("checkList2Add2~~~ ");
                                            //    }
                                            //}
                                            //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                            //qb03_count = 0;
                                            //foreach (var y in i.qb03)
                                            //{
                                            //    //////Console.WriteLine("yAA~~~ " + y);
                                            //    if (temp_j == y)
                                            //    {
                                            //        //////Console.WriteLine("temp_j~~ " + temp_j);
                                            //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                                            //        p = Convert.ToString(qb03_count);
                                            //    }
                                            //    qb03_count = qb03_count + 1;

                                            //}
                                            ////////Console.WriteLine("cc~~~ " + p);
                                            ////temp_value = TempAnsList.wqb99; // 之前的entry
                                            //var check2 = new checkInfo
                                            //{
                                            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            //    wqb01 = p,// 答案
                                            //              //wqb99 = TempAnsList.wqb99

                                            //};
                                        }
                                        // 跑選是的reset把checkList抓回來判斷
                                        //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                                        for (int a = 0; a < checkList.Count(); a++)
                                        {
                                            //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                            //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                            {
                                                ////////Console.WriteLine("IMMMM222~~~~");
                                                //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                                {
                                                    //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                    //foreach (var w in i.qb03)
                                                    for (int d = 0; d < i.qb03.Count(); d++)
                                                    {
                                                        //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                        //////Console.WriteLine("w00~~~ " + d.ToString());
                                                        if (checkList[a].wqb01 == d.ToString())
                                                        {

                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                            //////Console.WriteLine("j~~ " + j);
                                                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                            //ANS2 = Convert.ToString(qb03_count);
                                                            temp_j = i.qb03[d]; // 答案
                                                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                        }

                                                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                    }
                                                    // //////Console.WriteLine("cc~~~ " + p);
                                                    //////Console.WriteLine("ANS2~~ " + ANS2);

                                                    //temp_value = checkList[a].wqb99; // entry
                                                }
                                            }
                                        }



                                        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                     //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                        if (ischeck == true)
                                        {
                                            if (j == "是" || j == "已發")
                                            {
                                                isRed = true;
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                isGreen = true;
                                                isRed = false;
                                            }
                                        }
                                        //////Console.WriteLine("j3~~~ " + j);
                                        //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                                        //////Console.WriteLine("isRed3~~~ " + isRed);
                                        //////Console.WriteLine("isGreen3~~~ " + isGreen);

                                        //////Console.WriteLine("TFcount~~~" + TFcount);
                                        if (TFcount == 1)
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Red,

                                            };
                                        }
                                        else
                                        {
                                            check_box = new CheckBox // 產生checkbox
                                            {

                                                IsChecked = ischeck,
                                                Margin = new Thickness(-5, 0, 0, 0),
                                                //Color = Color.FromHex("264653")
                                                Color = Color.Green
                                            };
                                        }



                                        //if (j == "是")
                                        //{
                                        //entny = new Entry // 產生Entry
                                        //{
                                        //    Placeholder = "請說明",
                                        //    Text = temp_value,
                                        //    IsVisible = isEntry,
                                        //    IsEnabled = isEntry


                                        //};



                                        //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                                        //{
                                        //    for (int a = 0; a < checkList.Count(); a++)
                                        //    {
                                        //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                        //        {
                                        //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                                        //            {
                                        //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                                        //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                                        //                checkList[a].wqb99 = ee.NewTextValue;
                                        //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                                        //            }

                                        //        }

                                        //    }
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");
                                        //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                                        //    //        foreach (var TempAnsList in c)
                                        //    //        {
                                        //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                                        //    //            //////Console.WriteLine("WHO~~" + who);
                                        //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    for (int d = 0; d < checkList.Count(); d++)
                                        //    //                    {
                                        //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                                        //    //                        }

                                        //    //                    }

                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    //temp_value = TempAnsList.wqb99;
                                        //    //                    //var check2 = new checkInfo
                                        //    //                    //{
                                        //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                                        //    //                    //    wqb99 = TempAnsList.wqb99

                                        //    //                    //};

                                        //    //                    //checkList.Add(check2);

                                        //    //                    //MapView.AccDatabase.DeleteItem(b);
                                        //    //                }
                                        //    //            }


                                        //    //        }
                                        //    //    }



                                        //    //}
                                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                                        //    //{
                                        //    //    //////Console.WriteLine("IMMMM~~~~");

                                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                                        //    //    {
                                        //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                                        //    //        foreach (var TempAnsList in a)
                                        //    //        {
                                        //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        //    //            {
                                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                                        //    //                {
                                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                                        //    //                    //temp_j = TempAnsList.wqb01;
                                        //    //                    for (int c = 0; c < checkList.Count(); c++)
                                        //    //                    {
                                        //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                                        //    //                        {
                                        //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                                        //    //                        }
                                        //    //                    }
                                        //    //                }
                                        //    //            }

                                        //    //        }
                                        //    //    }


                                        //    //}
                                        //};
                                        //}
                                        check_box.CheckedChanged += async (s, e) =>
                                        {
                                            //////Console.WriteLine("checkboxin3~~~");
                                            if (e.Value) // 如果選是，要跳出entry所以需要reset
                                            {
                                                ////////Console.WriteLine("IN~~~");
                                                //ischeck = true;
                                                //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                    {
                                                        if (checkList[a].qb_s_num == i.qb_s_num)
                                                        {
                                                            checkList.RemoveAt(a);
                                                            //checkList2.RemoveAt(a);
                                                        }
                                                    }

                                                }
                                                ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //if (tmp_name_list.Contains(questionList.ClientName))
                                                //{
                                                //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                                //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                                //    //////Console.WriteLine("a~ " + total);
                                                //    tmp_name_list.Remove(questionList.ClientName);
                                                //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                                //    //////Console.WriteLine("b~ " + total2);
                                                //}
                                                //////Console.WriteLine("jjj~~~ " + j);
                                                if (j == "未發")
                                                {
                                                    //IsChoose = true;
                                                    CheckboxList[questionList.ClientName ] = true;
                                                    ChooseSaveToDB(questionList.ClientName, true);
                                                    //////Console.WriteLine("LLL~~~ " + IsChoose);
                                                }
                                                //if (j == "是" || j == "已發")
                                                //{
                                                //    ANS = 0;
                                                //    ANS2 = Convert.ToString(ANS);


                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS2 = Convert.ToString(ANS);

                                                //}
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    //////Console.WriteLine("j00~~ " + j);
                                                    //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                    if (j == i.qb03[d])
                                                    {

                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                        //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        //////Console.WriteLine("j~~ " + j);
                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        ANS2 = d.ToString();
                                                        //////Console.WriteLine("jj~~ " + temp_j);
                                                        //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                    }

                                                    //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS2~~ " + ANS2);
                                                //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS21~~~ " + ANS2);
                                                // 把問題選項存進資料庫
                                                ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                                //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                                //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                                ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                                ////////Console.WriteLine("j~~" + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    color = "Red";
                                                }
                                                else
                                                {
                                                    color = "Green";
                                                }
                                                //////Console.WriteLine("color~~~ " + color);
                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                                ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    wqb01 = ANS2 // 答案

                                                };
                                                checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                    qb_order = i.qb_order,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                                checkList2.Add(check3); // for save
                                                                        //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList.Add(check); // for check
                                                                      //checkList2.Add(check3); // for save
                                                                      ////////Console.WriteLine("CHECK" + checkList[0]);
                                                ///
                                                reset();
                                                //isReset = true;
                                                //if (j == "未發")
                                                //{
                                                //    reset();
                                                //}


                                                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                            }
                                            else // 選否則不用reset
                                            {
                                                //ischeck = false;
                                                //////Console.WriteLine("remove~~~~~");
                                                for (int a = 0; a < checkList.Count(); a++)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                    }
                                                }
                                            }

                                            //foreach (var b in checkList)
                                            //{
                                            //    //////Console.WriteLine("HERE~~");
                                            //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                            //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                            //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                            //    //////Console.WriteLine("enrty : " + b.wqb99);
                                            //}
                                        };
                                        //////Console.WriteLine("isReset~~~~ " + isReset);
                                        //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                                        //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        //////Console.WriteLine("WWWRRR_j~~~ " + j);
                                        ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                                        if (isReset == true || isDB == true)
                                        {
                                            if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL1~~~ " + j);
                                                if (j == "是" || j == "已發")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Red,
                                                        //Margin = new Thickness(-10,0,0,0),
                                                        FontSize = 20
                                                    };
                                                    isRed = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL1-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                            {
                                                //////Console.WriteLine("JKL2~~~ " + j);
                                                if (j == "未發" || j == "否")
                                                {
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Green,
                                                        //Margin = new Thickness(-10, 0, 0, 0),
                                                        FontSize = 20
                                                    };
                                                    isGreen = false;
                                                    //isReset = false;
                                                }
                                                else
                                                {
                                                    //////Console.WriteLine("JKL2-1~~~ " + j);
                                                    label_check = new Label // 選項
                                                    {
                                                        Text = j,
                                                        TextColor = Color.Black,
                                                        FontSize = 20
                                                    };
                                                }

                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL3~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }
                                        }
                                        else
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };
                                        }


                                        var stack_check = new StackLayout // checkbox跟選項
                                        {
                                            Orientation = StackOrientation.Horizontal,
                                            Children = { check_box, label_check }
                                        };

                                        //var ques_all_check = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_check, stack }
                                        //};

                                        //var stack_enrty = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Vertical,
                                        //    Children = { stack_check, entny }
                                        //};
                                        stack_ques.Children.Add(stack_check);
                                        //count = count + 1;

                                        //var final_stack = new StackLayout
                                        //{
                                        //    Orientation = StackOrientation.Horizontal,
                                        //    Children = { stack_ques, label_que_name }
                                        //};
                                        //}
                                    }

                                }
                            }
                        }




                        quesStack.Children.Add(stack); // w

                        //quesStack.Children.Add(final_stack);
                        //quesStack.Children.Add(label_que_name);
                        //quesStack.Children.Add(stack_ques);
                        var final_stack = new StackLayout
                        {

                            Orientation = StackOrientation.Vertical,
                            Children = { label_que_name, stack_ques }
                        };


                        Frame frame = new Frame // frame包上面那個stacklayout
                        {
                            Padding = new Thickness(10, 5, 10, 5),
                            Margin = new Thickness(5, 0, 5, 0),
                            BackgroundColor = Color.FromHex("eddcd2"),

                            CornerRadius = 10,
                            HasShadow = false,
                            Content = final_stack
                        };
                        //ScrollView scrollView = new ScrollView { 
                        //    Content = frame ,
                        //    Orientation = ScrollOrientation.Both
                        //};

                        quesStack.Children.Add(frame);

                        //var last_stack = new StackLayout
                        //{
                        //    Orientation = StackOrientation.Vertical,
                        //    Children = { stack, frame }
                        //};

                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                        //Content = scrollView;
                        //ScrollView scrollView = new ScrollView
                        //{
                        //    Content = quesStack

                        //};
                        //quesStack.Children.Add(scrollView);
                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                    }

                }
                else // CheckboxList[questionList.ClientName] == true，判斷是否選擇未發，觸發第四題
                {
                    //Console.WriteLine("inB~~~~ ");
                    //Console.WriteLine("name~~~ " + questionList.ClientName);
                    //Console.WriteLine("checkbox~~~ " + CheckboxList[questionList.ClientName + questionList.qb_s_num]);
                    if (CheckboxList[questionList.ClientName] == true)
                    {

                        if (i.qb02 == "1") // 問題類型(假設1是是否題 / 單選)(沒有entry版本)
                        {
                            Qtype = "1";
                            //string set = questionList.wqh_s_num + i.qb_s_num;
                            //TmpCheckList[set] = false;
                            var label_que_name = new Label // 問題題號+題目
                            {
                                Text = i.qb_order + " " + i.qb01,
                                FontSize = 20,
                                TextColor = Color.Black
                            };

                            var stack_ques = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal
                            };
                            //////Console.WriteLine("i.qb03~~~ " + i.qb03);
                            //////Console.WriteLine("i.qb_order~~~ " + i.qb_order);
                            //////Console.WriteLine("result~~~ " + result);
                            //////Console.WriteLine("TorF~~~" + result == "星期三");
                            //////Console.WriteLine("TorF2~~~~ " + result.Equals("星期三"));
                            //////Console.WriteLine("count~~~ " + questionList.qbs.Count());
                            //////Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                            ///
                            //if(i.qb_order != "4")
                            //{
                            //    foreach (var j in i.qb03)
                            //    {

                            //        if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期五") == true)
                            //        {
                            //            ////////Console.WriteLine("friday_in~~~ ");
                            //            //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            //            //{
                            //            //count = 0;
                            //            ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                            //            ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                            //            ////Console.WriteLine("order~~~ " + i.qb_order);
                            //            ////Console.WriteLine("result_num~~~ " + result_num);
                            //            TFcount = TFcount + 1;
                            //            var temp_j = "";
                            //            var temp_value = "";
                            //            //var temp_j_map = "";
                            //            //var temp_value_map = "";
                            //            ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                            //            if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                            //            {
                            //                ////////Console.WriteLine("first~~ ");
                            //                ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                            //                ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                var _wqhsnum = questionList.wqh_s_num;
                            //                temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                            //                ////////Console.WriteLine("tempj~~ " + temp_j);
                            //                for (int d = 0; d < i.qb03.Count(); d++)
                            //                {
                            //                    ////////Console.WriteLine("j00~~ " + j);
                            //                    ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                    if (temp_j == i.qb03[d])
                            //                    {

                            //                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                        //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                        ////////Console.WriteLine("j~~ " + j);
                            //                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                        //ANS2 = Convert.ToString(qb03_count);
                            //                        ANS2 = d.ToString();
                            //                        ////////Console.WriteLine("jj~~ " + temp_j);
                            //                        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                    }

                            //                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                }
                            //                ////////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                            //                ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                ////////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                            //                if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                            //                {
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save
                            //                    AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                            //                    TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    ////////Console.WriteLine("checkList2Add2~~~ ");
                            //                }

                            //                // ////////Console.WriteLine("cc~~~ " + p);
                            //                ////////Console.WriteLine("ANS2~~ " + ANS2);
                            //                //var ques = questionList.wqh_s_num + i.qb_order;
                            //                //////////Console.WriteLine("ques!!! " + ques);
                            //                //for(int e = 0; e < checkList2.Count(); e++)
                            //                //{
                            //                //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                            //                //    ////////Console.WriteLine("check!!! " + check);
                            //                //    if (check != ques)
                            //                //    {
                            //                //        ////////Console.WriteLine("checkin!!! " + check);
                            //                //        ////////Console.WriteLine("quesin!!! " + ques);
                            //                //        var check3 = new checkInfo
                            //                //        {
                            //                //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //            qb_order = i.qb_order,
                            //                //            wqb01 = ANS2 // 答案

                            //                //        };
                            //                //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                //        checkList2.Add(check3); // for save
                            //                //        ////////Console.WriteLine("checkList2Add2~~~ ");
                            //                //    }
                            //                //}
                            //                ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                            //                //qb03_count = 0;
                            //                //foreach (var y in i.qb03)
                            //                //{
                            //                //    ////////Console.WriteLine("yAA~~~ " + y);
                            //                //    if (temp_j == y)
                            //                //    {
                            //                //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                            //                //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //                //        p = Convert.ToString(qb03_count);
                            //                //    }
                            //                //    qb03_count = qb03_count + 1;

                            //                //}
                            //                //////////Console.WriteLine("cc~~~ " + p);
                            //                ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //                //var check2 = new checkInfo
                            //                //{
                            //                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //    wqb01 = p,// 答案
                            //                //              //wqb99 = TempAnsList.wqb99

                            //                //};
                            //            }
                            //            // 跑選是的reset把checkList抓回來判斷
                            //            ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                            //            for (int a = 0; a < checkList.Count(); a++)
                            //            {
                            //                ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                            //                ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                            //                //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                {
                            //                    //////////Console.WriteLine("IMMMM222~~~~");
                            //                    ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                            //                    if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                            //                    {
                            //                        ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                            //                        //foreach (var w in i.qb03)
                            //                        for (int d = 0; d < i.qb03.Count(); d++)
                            //                        {
                            //                            ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                            //                            ////////Console.WriteLine("w00~~~ " + d.ToString());
                            //                            if (checkList[a].wqb01 == d.ToString())
                            //                            {

                            //                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                            //                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                ////////Console.WriteLine("j~~ " + j);
                            //                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                //ANS2 = Convert.ToString(qb03_count);
                            //                                temp_j = i.qb03[d]; // 答案
                            //                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                            //                            }

                            //                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                        }
                            //                        // ////////Console.WriteLine("cc~~~ " + p);
                            //                        ////////Console.WriteLine("ANS2~~ " + ANS2);

                            //                        //temp_value = checkList[a].wqb99; // entry
                            //                    }
                            //                }
                            //            }
                            //            ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                            //            ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                            //            ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                            //            ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //            ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                            //            // ------------------------------------------

                            //            //////////Console.WriteLine("")
                            //            //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //            //~~~~~~~~~~~~~~~~~~~~~~~~
                            //            //qb03_count = 0;
                            //            //foreach (var y in i.qb03)
                            //            //{
                            //            //    ////////Console.WriteLine("yAA~~~ " + y);
                            //            //    if (temp_j == y)
                            //            //    {
                            //            //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                            //            //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //            //        p = Convert.ToString(qb03_count);
                            //            //    }
                            //            //    qb03_count = qb03_count + 1;

                            //            //}
                            //            //////////Console.WriteLine("cc~~~ " + p);
                            //            ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //            //var check2 = new checkInfo
                            //            //{
                            //            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //            //    wqb01 = p,// 答案
                            //            //              //wqb99 = TempAnsList.wqb99

                            //            //};
                            //            ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                            //            ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                            //            ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                            //            ////////////Console.WriteLine("ID" + TempAnsList.ID);
                            //            //////////Console.WriteLine("cc~~~ " + p);
                            //            //checkList.Add(check2);
                            //            //checkList2.Add(check2);
                            //            //~~~~~~~~~~~~~~~~~~~~`
                            //            ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                            //            ////////Console.WriteLine("j~~ " + j);

                            //            bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                            //                                                         //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                            //            if (ischeck == true)
                            //            {
                            //                //if (j == "是" || j == "已發")
                            //                //{
                            //                //    isRed = true;
                            //                //}
                            //                //else
                            //                //{
                            //                //    isGreen = true;
                            //                //}

                            //            }
                            //            ////////Console.WriteLine("j1~~~ " + j);
                            //            ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                            //            ////////Console.WriteLine("isRed1~~~ " + isRed);
                            //            ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                            //            ////////Console.WriteLine("TFcount~~~" + TFcount);
                            //            //if (j == "是" || j == "已發")
                            //            ////////Console.WriteLine("name~~ " + questionList.ClientName);
                            //            ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                            //            if (TFcount == 1)
                            //            {
                            //                ////////Console.WriteLine("Red~~ ");
                            //                check_box = new CheckBox // 產生checkbox
                            //                {

                            //                    IsChecked = ischeck,
                            //                    Margin = new Thickness(-5, 0, 0, 0),
                            //                    //Color = Color.FromHex("264653")
                            //                    Color = Color.Green
                            //                };
                            //            }
                            //            else
                            //            {
                            //                ////////Console.WriteLine("Green~~~ ");
                            //                check_box = new CheckBox // 產生checkbox
                            //                {

                            //                    IsChecked = ischeck,
                            //                    Margin = new Thickness(-5, 0, 0, 0),
                            //                    //Color = Color.FromHex("264653")
                            //                    Color = Color.Red
                            //                };
                            //            }



                            //            //if (j == "是")
                            //            //{
                            //            //entny = new Entry // 產生Entry
                            //            //{
                            //            //    Placeholder = "請說明",
                            //            //    Text = temp_value,
                            //            //    IsVisible = isEntry,
                            //            //    IsEnabled = isEntry


                            //            //};



                            //            //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                            //            //{
                            //            //    for (int a = 0; a < checkList.Count(); a++)
                            //            //    {
                            //            //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //            //        {
                            //            //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                            //            //            {
                            //            //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                            //            //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                            //            //                checkList[a].wqb99 = ee.NewTextValue;
                            //            //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                            //            //            }

                            //            //        }

                            //            //    }
                            //            //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //            //    //{
                            //            //    //    ////////Console.WriteLine("IMMMM~~~~");
                            //            //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //            //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //            //    //    {
                            //            //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                            //            //    //        foreach (var TempAnsList in c)
                            //            //    //        {
                            //            //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                            //            //    //            ////////Console.WriteLine("WHO~~" + who);
                            //            //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                            //            //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //            //    //            {
                            //            //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //            //    //                {
                            //            //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //            //    //                    for (int d = 0; d < checkList.Count(); d++)
                            //            //    //                    {
                            //            //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                            //            //    //                        {
                            //            //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                            //            //    //                        }

                            //            //    //                    }

                            //            //    //                    //temp_j = TempAnsList.wqb01;
                            //            //    //                    //temp_value = TempAnsList.wqb99;
                            //            //    //                    //var check2 = new checkInfo
                            //            //    //                    //{
                            //            //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //            //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                            //            //    //                    //    wqb99 = TempAnsList.wqb99

                            //            //    //                    //};

                            //            //    //                    //checkList.Add(check2);

                            //            //    //                    //MapView.AccDatabase.DeleteItem(b);
                            //            //    //                }
                            //            //    //            }


                            //            //    //        }
                            //            //    //    }



                            //            //    //}
                            //            //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //            //    //{
                            //            //    //    ////////Console.WriteLine("IMMMM~~~~");

                            //            //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //            //    //    {
                            //            //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                            //            //    //        foreach (var TempAnsList in a)
                            //            //    //        {
                            //            //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                            //            //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //            //    //            {
                            //            //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //            //    //                {
                            //            //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //            //    //                    //temp_j = TempAnsList.wqb01;
                            //            //    //                    for (int c = 0; c < checkList.Count(); c++)
                            //            //    //                    {
                            //            //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                            //            //    //                        {
                            //            //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                            //            //    //                        }
                            //            //    //                    }
                            //            //    //                }
                            //            //    //            }

                            //            //    //        }
                            //            //    //    }


                            //            //    //}
                            //            //};
                            //            //}
                            //            check_box.CheckedChanged += async (s, e) =>
                            //            {
                            //                ////////Console.WriteLine("checkboxin1~~~");
                            //                if (e.Value) // 如果選是，要跳出entry所以需要reset
                            //                {
                            //                    //////////Console.WriteLine("IN~~~");
                            //                    //ischeck = true;
                            //                    //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    for (int a = 0; a < checkList.Count(); a++)
                            //                    {
                            //                        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                        {
                            //                            if (checkList[a].qb_s_num == i.qb_s_num)
                            //                            {
                            //                                checkList.RemoveAt(a);
                            //                                //checkList2.RemoveAt(a);
                            //                            }
                            //                        }

                            //                    }
                            //                    //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                    //if (tmp_name_list.Contains(questionList.ClientName))
                            //                    //{
                            //                    //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                    //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                            //                    //    ////////Console.WriteLine("a~ " + total);
                            //                    //    tmp_name_list.Remove(questionList.ClientName);
                            //                    //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                            //                    //    ////////Console.WriteLine("b~ " + total2);
                            //                    //}
                            //                    ////////Console.WriteLine("jjj~~~ " + j);
                            //                    if (j == "未發")
                            //                    {
                            //                        //IsChoose = true;
                            //                        CheckboxList[questionList.ClientName + questionList.qb_s_num] = true;
                            //                        ChooseSaveToDB(questionList.ClientName, true);
                            //                        ////////Console.WriteLine("LLL~~~ " + IsChoose);
                            //                        ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                            //                    }
                            //                    //if (j == "是")
                            //                    //{
                            //                    //    ANS = 0;
                            //                    //    ANS2 = Convert.ToString(ANS);
                            //                    //}
                            //                    //else
                            //                    //{
                            //                    //    ANS = 1;
                            //                    //    ANS2 = Convert.ToString(ANS);
                            //                    //}
                            //                    ////////Console.WriteLine("j~~~ " + j);
                            //                    //qb03_count = 0;
                            //                    //foreach(var w in i.qb03)
                            //                    //{
                            //                    //    if(j == w)
                            //                    //    {
                            //                    //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                    //        ////////Console.WriteLine("j~~ " + j);
                            //                    //        ////////Console.WriteLine("w~~~ " + w);

                            //                    //        ANS2 = Convert.ToString(qb03_count);
                            //                    //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                    //    }
                            //                    //    qb03_count = qb03_count + 1;
                            //                    //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    //}
                            //                    for (int d = 0; d < i.qb03.Count(); d++)
                            //                    {
                            //                        ////////Console.WriteLine("j00~~ " + j);
                            //                        ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                        if (j == i.qb03[d])
                            //                        {

                            //                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                            ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                            ////////Console.WriteLine("j~~ " + j);
                            //                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //ANS2 = Convert.ToString(qb03_count);
                            //                            ANS2 = d.ToString();
                            //                            ////////Console.WriteLine("jj~~ " + temp_j);
                            //                            ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                        }

                            //                        ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    }
                            //                    // ////////Console.WriteLine("cc~~~ " + p);
                            //                    ////////Console.WriteLine("ANS2~~ " + ANS2);
                            //                    //var check3 = new checkInfo
                            //                    //{
                            //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //    qb_order = i.qb_order,
                            //                    //    wqb01 = ANS2 // 答案

                            //                    //};
                            //                    //////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //checkList2.Add(check3); // for save
                            //                    ////////Console.WriteLine("checkList2Add1~~~ ");

                            //                    //foreach (var k in checkList2)
                            //                    //{
                            //                    ////////Console.WriteLine("a~ " + k.wqh_s_num);
                            //                    ////////Console.WriteLine("b~ " + k.wqb01);
                            //                    //}
                            //                    ////////Console.WriteLine("count2~~ " + checkList2.Count());
                            //                    // 把問題選項存進資料庫
                            //                    //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                            //                    ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                            //                    ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                            //                    //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                            //                    //////////Console.WriteLine("j~~" + j);
                            //                    if (j == "是" || j == "未發")
                            //                    {
                            //                        color = "Red";
                            //                    }
                            //                    else
                            //                    {
                            //                        color = "Green";
                            //                    }
                            //                    ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                            //                    ////////Console.WriteLine("color~~~ " + color);
                            //                    ////////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                            //                    QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);

                            //                    ////////Console.WriteLine("j_HERE~~~ " + j);
                            //                    var check = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save

                            //                    ////////Console.WriteLine("wqb01~~ " + ANS2);
                            //                    ////////Console.WriteLine("j_HERE2~~~ " + j);
                            //                    ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //                    checkList.Add(check); // for check

                            //                    //////////Console.WriteLine("CHECK" + checkList[0]);
                            //                    ////////Console.WriteLine("checkList2ADD~~ ");

                            //                    //if (j == "未發")
                            //                    //{
                            //                    //    ////Console.WriteLine("reset~friday~~ ");
                            //                    //    reset();
                            //                    //}
                            //                    reset();
                            //                    isReset = true;
                            //                    ////Console.WriteLine("ISREST~~~ " + isReset);
                            //                    // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                            //                }
                            //                else //
                            //                {
                            //                    //ischeck = false;
                            //                    ////////Console.WriteLine("remove~~~~~");
                            //                    for (int a = 0; a < checkList.Count(); a++)
                            //                    {
                            //                        if (checkList[a].qb_s_num == i.qb_s_num)
                            //                        {
                            //                            checkList.RemoveAt(a);
                            //                        }
                            //                    }
                            //                }

                            //                //foreach (var b in checkList)
                            //                //{
                            //                //    ////////Console.WriteLine("HERE~~");
                            //                //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                            //                //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                            //                //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                            //                //    ////////Console.WriteLine("enrty : " + b.wqb99);
                            //                //}
                            //            };
                            //            ////Console.WriteLine("isreset~~~ ");
                            //            ////Console.WriteLine("isRest~~ " + isReset);
                            //            if (isReset == true || isDB == true)
                            //            {
                            //                if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                {
                            //                    ////Console.WriteLine("A~~~ " + j);
                            //                    if (j == "是" || j == "未發")
                            //                    {
                            //                        ////Console.WriteLine("A-1~~~ ");
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Red,
                            //                            FontSize = 20
                            //                        };
                            //                        isRed = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        ////Console.WriteLine("A-2~~~ ");
                            //                        ////////Console.WriteLine("JKL1+1~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                {
                            //                    ////Console.WriteLine("B~~~~ " + j);
                            //                    if (j == "已發" || j == "否")
                            //                    {
                            //                        ////Console.WriteLine("B-1~~~ ");
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Green,
                            //                            FontSize = 20
                            //                        };
                            //                        isGreen = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        ////Console.WriteLine("B-2~~~ ");
                            //                        ////////Console.WriteLine("JKL2-1~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    ////Console.WriteLine("C~~~ " + j);
                            //                    ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                            //                    ////Console.WriteLine("j~~~ " + j);
                            //                    label_check = new Label // 選項
                            //                    {
                            //                        Text = j,
                            //                        TextColor = Color.Black,
                            //                        FontSize = 20
                            //                    };

                            //                }
                            //            }
                            //            else
                            //            {
                            //                ////Console.WriteLine("D~~~ ");
                            //                label_check = new Label // 選項
                            //                {
                            //                    Text = j,
                            //                    TextColor = Color.Black,
                            //                    FontSize = 20
                            //                };
                            //            }


                            //            var stack_check = new StackLayout // checkbox跟選項
                            //            {
                            //                Orientation = StackOrientation.Horizontal,
                            //                Children = { check_box, label_check }
                            //            };

                            //            //var ques_all_check = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Horizontal,
                            //            //    Children = { stack_check, stack }
                            //            //};

                            //            //var stack_enrty = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Vertical,
                            //            //    Children = { stack_check, entny }
                            //            //};
                            //            stack_ques.Children.Add(stack_check);

                            //            //count = count + 1;

                            //            //var final_stack = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Horizontal,
                            //            //    Children = { stack_ques, label_que_name }
                            //            //};
                            //            //}
                            //        }
                            //        else if (i.qb_order == "3" && questionList.qbs.Count() == 5 && result.Equals("星期四") == true)
                            //        {
                            //            //Console.WriteLine("thusday_in~~~ ");
                            //            //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            //            //{
                            //            //count = 0;
                            //            ////Console.WriteLine("nameAA~~~ " + questionList.ClientName);
                            //            ////Console.WriteLine("countAA~~~ " + questionList.qbs.Count());
                            //            ////Console.WriteLine("order~~~ " + i.qb_order);
                            //            ////Console.WriteLine("result_num~~~ " + result_num);
                            //            TFcount = TFcount + 1;
                            //            var temp_j = "";
                            //            var temp_value = "";
                            //            //var temp_j_map = "";
                            //            //var temp_value_map = "";
                            //            ////////Console.WriteLine("checklist2~count1~ " + checkList2.Count());
                            //            if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                            //            {
                            //                ////////Console.WriteLine("first~~ ");
                            //                ////////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                            //                ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                var _wqhsnum = questionList.wqh_s_num;
                            //                temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                            //                ////////Console.WriteLine("tempj~~ " + temp_j);
                            //                for (int d = 0; d < i.qb03.Count(); d++)
                            //                {
                            //                    ////////Console.WriteLine("j00~~ " + j);
                            //                    ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                    if (temp_j == i.qb03[d])
                            //                    {

                            //                        ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                        //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                        //Console.WriteLine("j~~ " + j);
                            //                        //Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                        //ANS2 = Convert.ToString(qb03_count);
                            //                        ANS2 = d.ToString();
                            //                        ////////Console.WriteLine("jj~~ " + temp_j);
                            //                        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                    }

                            //                    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                }
                            //                //Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                            //                //Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                //Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]); 
                            //                if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                            //                {
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save
                            //                    AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                            //                    TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    ////////Console.WriteLine("checkList2Add2~~~ ");
                            //                }

                            //                // ////////Console.WriteLine("cc~~~ " + p);
                            //                ////////Console.WriteLine("ANS2~~ " + ANS2);
                            //                //var ques = questionList.wqh_s_num + i.qb_order;
                            //                //////////Console.WriteLine("ques!!! " + ques);
                            //                //for(int e = 0; e < checkList2.Count(); e++)
                            //                //{
                            //                //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                            //                //    ////////Console.WriteLine("check!!! " + check);
                            //                //    if (check != ques)
                            //                //    {
                            //                //        ////////Console.WriteLine("checkin!!! " + check);
                            //                //        ////////Console.WriteLine("quesin!!! " + ques);
                            //                //        var check3 = new checkInfo
                            //                //        {
                            //                //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //            qb_order = i.qb_order,
                            //                //            wqb01 = ANS2 // 答案

                            //                //        };
                            //                //        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                //        checkList2.Add(check3); // for save
                            //                //        ////////Console.WriteLine("checkList2Add2~~~ ");
                            //                //    }
                            //                //}
                            //                ////////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                            //                //qb03_count = 0;
                            //                //foreach (var y in i.qb03)
                            //                //{
                            //                //    ////////Console.WriteLine("yAA~~~ " + y);
                            //                //    if (temp_j == y)
                            //                //    {
                            //                //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                            //                //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //                //        p = Convert.ToString(qb03_count);
                            //                //    }
                            //                //    qb03_count = qb03_count + 1;

                            //                //}
                            //                //////////Console.WriteLine("cc~~~ " + p);
                            //                ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //                //var check2 = new checkInfo
                            //                //{
                            //                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //    wqb01 = p,// 答案
                            //                //              //wqb99 = TempAnsList.wqb99

                            //                //};
                            //            }
                            //            // 跑選是的reset把checkList抓回來判斷
                            //            ////////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                            //            for (int a = 0; a < checkList.Count(); a++)
                            //            {
                            //                ////////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                            //                ////////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                            //                //////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                {
                            //                    //////////Console.WriteLine("IMMMM222~~~~");
                            //                    ////////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                            //                    if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                            //                    {
                            //                        ////////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                            //                        //foreach (var w in i.qb03)
                            //                        for (int d = 0; d < i.qb03.Count(); d++)
                            //                        {
                            //                            ////////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                            //                            ////////Console.WriteLine("w00~~~ " + d.ToString());
                            //                            if (checkList[a].wqb01 == d.ToString())
                            //                            {

                            //                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                ////////Console.WriteLine("check~~ " + checkList[a].wqb01);
                            //                                ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                ////////Console.WriteLine("j~~ " + j);
                            //                                ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                //ANS2 = Convert.ToString(qb03_count);
                            //                                temp_j = i.qb03[d]; // 答案
                            //                                                    ////////Console.WriteLine("jj~~ " + temp_j);
                            //                            }

                            //                            ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                        }
                            //                        // ////////Console.WriteLine("cc~~~ " + p);
                            //                        ////////Console.WriteLine("ANS2~~ " + ANS2);

                            //                        //temp_value = checkList[a].wqb99; // entry
                            //                    }
                            //                }
                            //            }
                            //            ////////Console.WriteLine("checklist2~count4~ " + checkList2.Count());
                            //            ////////Console.WriteLine("tmeppppp_j~~~ " + temp_j);
                            //            ////////Console.WriteLine("wqh111111~~ " + questionList.wqh_s_num);
                            //            ////////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //            ////////Console.WriteLine("ans~ " + TmpAnsList[questionList.ClientName + i.qb_order]);


                            //            // ------------------------------------------

                            //            //////////Console.WriteLine("")
                            //            //////////Console.WriteLine("DATACOUNT~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //            //~~~~~~~~~~~~~~~~~~~~~~~~
                            //            //qb03_count = 0;
                            //            //foreach (var y in i.qb03)
                            //            //{
                            //            //    ////////Console.WriteLine("yAA~~~ " + y);
                            //            //    if (temp_j == y)
                            //            //    {
                            //            //        ////////Console.WriteLine("temp_j~~ " + temp_j);
                            //            //        ////////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //            //        p = Convert.ToString(qb03_count);
                            //            //    }
                            //            //    qb03_count = qb03_count + 1;

                            //            //}
                            //            //////////Console.WriteLine("cc~~~ " + p);
                            //            ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //            //var check2 = new checkInfo
                            //            //{
                            //            //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //            //    wqb01 = p,// 答案
                            //            //              //wqb99 = TempAnsList.wqb99

                            //            //};
                            //            ////////////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                            //            ////////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            ////////////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                            //            ////////////Console.WriteLine("answer " + TempAnsList.wqb01);
                            //            ////////////Console.WriteLine("ID" + TempAnsList.ID);
                            //            //////////Console.WriteLine("cc~~~ " + p);
                            //            //checkList.Add(check2);
                            //            //checkList2.Add(check2);
                            //            //~~~~~~~~~~~~~~~~~~~~`
                            //            ////////Console.WriteLine("tempj~~LA~~ " + temp_j);
                            //            ////////Console.WriteLine("j~~ " + j);

                            //            bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                            //                                                         //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                            //            if (ischeck == true)
                            //            {
                            //                //if (j == "是" || j == "已發")
                            //                //{
                            //                //    isRed = true;
                            //                //}
                            //                //else
                            //                //{
                            //                //    isGreen = true;
                            //                //}

                            //            }
                            //            ////////Console.WriteLine("j1~~~ " + j);
                            //            ////////Console.WriteLine("isckeck1~~~~ " + ischeck);
                            //            ////////Console.WriteLine("isRed1~~~ " + isRed);
                            //            ////////Console.WriteLine("isGreen1~~~ " + isGreen);

                            //            ////////Console.WriteLine("TFcount~~~" + TFcount);
                            //            //if (j == "是" || j == "已發")
                            //            ////////Console.WriteLine("name~~ " + questionList.ClientName);
                            //            ////////Console.WriteLine("GorR~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                            //            if (TFcount == 1)
                            //            {
                            //                ////////Console.WriteLine("Red~~ ");
                            //                check_box = new CheckBox // 產生checkbox
                            //                {

                            //                    IsChecked = ischeck,
                            //                    Margin = new Thickness(-5, 0, 0, 0),
                            //                    //Color = Color.FromHex("264653")
                            //                    Color = Color.Green
                            //                };
                            //            }
                            //            else
                            //            {
                            //                ////////Console.WriteLine("Green~~~ ");
                            //                check_box = new CheckBox // 產生checkbox
                            //                {

                            //                    IsChecked = ischeck,
                            //                    Margin = new Thickness(-5, 0, 0, 0),
                            //                    //Color = Color.FromHex("264653")
                            //                    Color = Color.Red
                            //                };
                            //            }



                            //            //if (j == "是")
                            //            //{
                            //            //entny = new Entry // 產生Entry
                            //            //{
                            //            //    Placeholder = "請說明",
                            //            //    Text = temp_value,
                            //            //    IsVisible = isEntry,
                            //            //    IsEnabled = isEntry


                            //            //};



                            //            //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                            //            //{
                            //            //    for (int a = 0; a < checkList.Count(); a++)
                            //            //    {
                            //            //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //            //        {
                            //            //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                            //            //            {
                            //            //                ////////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                            //            //                ////////Console.WriteLine("Qbnum~" + i.qb_s_num);

                            //            //                checkList[a].wqb99 = ee.NewTextValue;
                            //            //                ////////Console.WriteLine("Text~" + ee.NewTextValue);
                            //            //            }

                            //            //        }

                            //            //    }
                            //            //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //            //    //{
                            //            //    //    ////////Console.WriteLine("IMMMM~~~~");
                            //            //    //    ////////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //            //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //            //    //    {
                            //            //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                            //            //    //        foreach (var TempAnsList in c)
                            //            //    //        {
                            //            //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                            //            //    //            ////////Console.WriteLine("WHO~~" + who);
                            //            //    //            ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                            //            //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //            //    //            {
                            //            //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //            //    //                {
                            //            //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //            //    //                    for (int d = 0; d < checkList.Count(); d++)
                            //            //    //                    {
                            //            //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                            //            //    //                        {
                            //            //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                            //            //    //                        }

                            //            //    //                    }

                            //            //    //                    //temp_j = TempAnsList.wqb01;
                            //            //    //                    //temp_value = TempAnsList.wqb99;
                            //            //    //                    //var check2 = new checkInfo
                            //            //    //                    //{
                            //            //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //            //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                            //            //    //                    //    wqb99 = TempAnsList.wqb99

                            //            //    //                    //};

                            //            //    //                    //checkList.Add(check2);

                            //            //    //                    //MapView.AccDatabase.DeleteItem(b);
                            //            //    //                }
                            //            //    //            }


                            //            //    //        }
                            //            //    //    }



                            //            //    //}
                            //            //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //            //    //{
                            //            //    //    ////////Console.WriteLine("IMMMM~~~~");

                            //            //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //            //    //    {
                            //            //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                            //            //    //        foreach (var TempAnsList in a)
                            //            //    //        {
                            //            //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                            //            //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //            //    //            {
                            //            //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //            //    //                {
                            //            //    //                    ////////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //            //    //                    ////////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //            //    //                    //temp_j = TempAnsList.wqb01;
                            //            //    //                    for (int c = 0; c < checkList.Count(); c++)
                            //            //    //                    {
                            //            //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                            //            //    //                        {
                            //            //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                            //            //    //                        }
                            //            //    //                    }
                            //            //    //                }
                            //            //    //            }

                            //            //    //        }
                            //            //    //    }


                            //            //    //}
                            //            //};
                            //            //}
                            //            check_box.CheckedChanged += async (s, e) =>
                            //            {
                            //                ////////Console.WriteLine("checkboxin1~~~");
                            //                if (e.Value) // 如果選是，要跳出entry所以需要reset
                            //                {
                            //                    //////////Console.WriteLine("IN~~~");
                            //                    //ischeck = true;
                            //                    //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    for (int a = 0; a < checkList.Count(); a++)
                            //                    {
                            //                        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                        {
                            //                            if (checkList[a].qb_s_num == i.qb_s_num)
                            //                            {
                            //                                checkList.RemoveAt(a);
                            //                                //checkList2.RemoveAt(a);
                            //                            }
                            //                        }

                            //                    }
                            //                    //////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                    //if (tmp_name_list.Contains(questionList.ClientName))
                            //                    //{
                            //                    //    ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                    //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                            //                    //    ////////Console.WriteLine("a~ " + total);
                            //                    //    tmp_name_list.Remove(questionList.ClientName);
                            //                    //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                            //                    //    ////////Console.WriteLine("b~ " + total2);
                            //                    //}
                            //                    ////////Console.WriteLine("jjj~~~ " + j);
                            //                    if (j == "未發")
                            //                    {
                            //                        //IsChoose = true;
                            //                        CheckboxList[questionList.ClientName + questionList.qb_s_num] = true;
                            //                        ChooseSaveToDB(questionList.ClientName, true);
                            //                        ////////Console.WriteLine("LLL~~~ " + IsChoose);
                            //                        ////////Console.WriteLine("checkboxList~~~ " + CheckboxList[questionList.ClientName]);
                            //                    }
                            //                    //if (j == "是")
                            //                    //{
                            //                    //    ANS = 0;
                            //                    //    ANS2 = Convert.ToString(ANS);
                            //                    //}
                            //                    //else
                            //                    //{
                            //                    //    ANS = 1;
                            //                    //    ANS2 = Convert.ToString(ANS);
                            //                    //}
                            //                    ////////Console.WriteLine("j~~~ " + j);
                            //                    //qb03_count = 0;
                            //                    //foreach(var w in i.qb03)
                            //                    //{
                            //                    //    if(j == w)
                            //                    //    {
                            //                    //        ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                    //        ////////Console.WriteLine("j~~ " + j);
                            //                    //        ////////Console.WriteLine("w~~~ " + w);

                            //                    //        ANS2 = Convert.ToString(qb03_count);
                            //                    //        ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                    //    }
                            //                    //    qb03_count = qb03_count + 1;
                            //                    //    ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    //}
                            //                    for (int d = 0; d < i.qb03.Count(); d++)
                            //                    {
                            //                        ////////Console.WriteLine("j00~~ " + j);
                            //                        ////////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                        if (j == i.qb03[d])
                            //                        {

                            //                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                            ////////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                            ////////Console.WriteLine("j~~ " + j);
                            //                            ////////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //ANS2 = Convert.ToString(qb03_count);
                            //                            ANS2 = d.ToString();
                            //                            ////////Console.WriteLine("jj~~ " + temp_j);
                            //                            ////////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                        }

                            //                        ////////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    }
                            //                    // ////////Console.WriteLine("cc~~~ " + p);
                            //                    ////////Console.WriteLine("ANS2~~ " + ANS2);
                            //                    //var check3 = new checkInfo
                            //                    //{
                            //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //    qb_order = i.qb_order,
                            //                    //    wqb01 = ANS2 // 答案

                            //                    //};
                            //                    //////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //checkList2.Add(check3); // for save
                            //                    ////////Console.WriteLine("checkList2Add1~~~ ");

                            //                    //foreach (var k in checkList2)
                            //                    //{
                            //                    ////////Console.WriteLine("a~ " + k.wqh_s_num);
                            //                    ////////Console.WriteLine("b~ " + k.wqb01);
                            //                    //}
                            //                    ////////Console.WriteLine("count2~~ " + checkList2.Count());
                            //                    // 把問題選項存進資料庫
                            //                    //////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                            //                    ////////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                            //                    ////////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                            //                    //////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                            //                    //////////Console.WriteLine("j~~" + j);
                            //                    if (j == "是" || j == "未發")
                            //                    {
                            //                        color = "Red";
                            //                    }
                            //                    else
                            //                    {
                            //                        color = "Green";
                            //                    }
                            //                    ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                            //                    //Console.WriteLine("color~~~ " + color);
                            //                    //Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                            //                    QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);

                            //                    //Console.WriteLine("j_HERE~~~ " + j);
                            //                    var check = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save

                            //                    ////////Console.WriteLine("wqb01~~ " + ANS2);
                            //                    ////////Console.WriteLine("j_HERE2~~~ " + j);
                            //                    ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //                    checkList.Add(check); // for check

                            //                    //////////Console.WriteLine("CHECK" + checkList[0]);
                            //                    ////////Console.WriteLine("checkList2ADD~~ ");

                            //                    //if (j == "未發")
                            //                    //{
                            //                    //    ////Console.WriteLine("reset~friday~~ ");
                            //                    //    reset();
                            //                    //}
                            //                    reset();
                            //                    isReset = true;
                            //                    ////Console.WriteLine("ISREST~~~ " + isReset);
                            //                    // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                            //                }
                            //                else //
                            //                {
                            //                    //ischeck = false;
                            //                    ////////Console.WriteLine("remove~~~~~");
                            //                    for (int a = 0; a < checkList.Count(); a++)
                            //                    {
                            //                        if (checkList[a].qb_s_num == i.qb_s_num)
                            //                        {
                            //                            checkList.RemoveAt(a);
                            //                        }
                            //                    }
                            //                }

                            //                //foreach (var b in checkList)
                            //                //{
                            //                //    ////////Console.WriteLine("HERE~~");
                            //                //    ////////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                            //                //    ////////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                            //                //    ////////Console.WriteLine("qb03 : " + b.wqb01);
                            //                //    ////////Console.WriteLine("enrty : " + b.wqb99);
                            //                //}
                            //            };
                            //            ////Console.WriteLine("isreset~~~ ");
                            //            ////Console.WriteLine("isRest~~ " + isReset);
                            //            //Console.WriteLine("bingoname22~~~ " + questionList.wqh_s_num);
                            //            //Console.WriteLine("qborder22~~ " + i.qb_order);
                            //            //Console.WriteLine("color22~~~ " + IsGreenOrRed[questionList.wqh_s_num + i.qb_order]);
                            //            if (isReset == true || isDB == true)
                            //            {
                            //                if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                {
                            //                    //Console.WriteLine("A~~~ " + j);
                            //                    if (j == "是" || j == "未發")
                            //                    {
                            //                        ////Console.WriteLine("A-1~~~ ");
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Red,
                            //                            FontSize = 20
                            //                        };
                            //                        isRed = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        ////Console.WriteLine("A-2~~~ ");
                            //                        ////////Console.WriteLine("JKL1+1~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                {
                            //                    //Console.WriteLine("B~~~~ " + j);
                            //                    if (j == "已發" || j == "否")
                            //                    {
                            //                        //Console.WriteLine("B-1~~~ ");
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Green,
                            //                            FontSize = 20
                            //                        };
                            //                        isGreen = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        ////Console.WriteLine("B-2~~~ ");
                            //                        ////////Console.WriteLine("JKL2-1~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    ////Console.WriteLine("C~~~ " + j);
                            //                    ////Console.WriteLine("ques~~ " + questionList.qbs[2].qb03[0]);
                            //                    ////Console.WriteLine("j~~~ " + j);
                            //                    label_check = new Label // 選項
                            //                    {
                            //                        Text = j,
                            //                        TextColor = Color.Black,
                            //                        FontSize = 20
                            //                    };

                            //                }
                            //            }
                            //            else
                            //            {
                            //                ////Console.WriteLine("D~~~ ");
                            //                label_check = new Label // 選項
                            //                {
                            //                    Text = j,
                            //                    TextColor = Color.Black,
                            //                    FontSize = 20
                            //                };
                            //            }


                            //            var stack_check = new StackLayout // checkbox跟選項
                            //            {
                            //                Orientation = StackOrientation.Horizontal,
                            //                Children = { check_box, label_check }
                            //            };

                            //            //var ques_all_check = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Horizontal,
                            //            //    Children = { stack_check, stack }
                            //            //};

                            //            //var stack_enrty = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Vertical,
                            //            //    Children = { stack_check, entny }
                            //            //};
                            //            stack_ques.Children.Add(stack_check);

                            //            //count = count + 1;

                            //            //var final_stack = new StackLayout
                            //            //{
                            //            //    Orientation = StackOrientation.Horizontal,
                            //            //    Children = { stack_ques, label_que_name }
                            //            //};
                            //            //}
                            //        }
                            //        else
                            //        {
                            //            if (i.qb_order == "1")
                            //            {
                            //                //Console.WriteLine("qborder1~~in~~~");
                            //                //Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                            //                //Console.WriteLine("name~~ " + questionList.ClientName);
                            //                //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            //                //{
                            //                //count = 0;
                            //                ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                            //                ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                            //                ////Console.WriteLine("order~~~ " + i.qb_order);
                            //                ////Console.WriteLine("result_num~~~ " + result_num);
                            //                TFcount = TFcount + 1;
                            //                var temp_j = "";
                            //                var temp_value = "";
                            //                //var temp_j_map = "";
                            //                //var temp_value_map = "";

                            //                // 跑選是的reset把checkList抓回來判斷
                            //                if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                            //                {
                            //                    //////Console.WriteLine("third~~ ");
                            //                    //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                            //                    //////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                    var _wqhsnum = questionList.wqh_s_num;
                            //                    temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                            //                    //////Console.WriteLine("tempj~~ " + temp_j);
                            //                    for (int d = 0; d < i.qb03.Count(); d++)
                            //                    {
                            //                        //////Console.WriteLine("j00~~ " + j);
                            //                        //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                        if (temp_j == i.qb03[d])
                            //                        {

                            //                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                            //////Console.WriteLine("j~~ " + j);
                            //                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //ANS2 = Convert.ToString(qb03_count);
                            //                            ANS2 = d.ToString();
                            //                            //////Console.WriteLine("jj~~ " + temp_j);
                            //                            //////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                        }

                            //                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    }
                            //                    //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                            //                    //////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                    //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save
                            //                                            //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                            //                                            //{
                            //                                            //    //////Console.WriteLine("ans2~~~ " + ANS2);
                            //                                            //    var check3 = new checkInfo
                            //                                            //    {
                            //                                            //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                                            //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                                            //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                                            //        qb_order = i.qb_order,
                            //                                            //        wqb01 = ANS2 // 答案

                            //                    //    };
                            //                    //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //    checkList2.Add(check3); // for save
                            //                    //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                            //                    //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                            //                    //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    //    //////Console.WriteLine("checkList2Add99~~~ ");
                            //                    //}

                            //                    // //////Console.WriteLine("cc~~~ " + p);
                            //                    //////Console.WriteLine("ANS2~~ " + ANS2);
                            //                    //var ques = questionList.wqh_s_num + i.qb_order;
                            //                    ////////Console.WriteLine("ques!!! " + ques);
                            //                    //for(int e = 0; e < checkList2.Count(); e++)
                            //                    //{
                            //                    //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                            //                    //    //////Console.WriteLine("check!!! " + check);
                            //                    //    if (check != ques)
                            //                    //    {
                            //                    //        //////Console.WriteLine("checkin!!! " + check);
                            //                    //        //////Console.WriteLine("quesin!!! " + ques);
                            //                    //        var check3 = new checkInfo
                            //                    //        {
                            //                    //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //            qb_order = i.qb_order,
                            //                    //            wqb01 = ANS2 // 答案

                            //                    //        };
                            //                    //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //        checkList2.Add(check3); // for save
                            //                    //        //////Console.WriteLine("checkList2Add2~~~ ");
                            //                    //    }
                            //                    //}
                            //                    //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                            //                    //qb03_count = 0;
                            //                    //foreach (var y in i.qb03)
                            //                    //{
                            //                    //    //////Console.WriteLine("yAA~~~ " + y);
                            //                    //    if (temp_j == y)
                            //                    //    {
                            //                    //        //////Console.WriteLine("temp_j~~ " + temp_j);
                            //                    //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //                    //        p = Convert.ToString(qb03_count);
                            //                    //    }
                            //                    //    qb03_count = qb03_count + 1;

                            //                    //}
                            //                    ////////Console.WriteLine("cc~~~ " + p);
                            //                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //                    //var check2 = new checkInfo
                            //                    //{
                            //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //    wqb01 = p,// 答案
                            //                    //              //wqb99 = TempAnsList.wqb99

                            //                    //};
                            //                }
                            //                // 跑選是的reset把checkList抓回來判斷
                            //                //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                            //                for (int a = 0; a < checkList.Count(); a++)
                            //                {
                            //                    //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                            //                    //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                            //                    ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                    if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                    {
                            //                        ////////Console.WriteLine("IMMMM222~~~~");
                            //                        //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                            //                        if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                            //                        {
                            //                            //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                            //                            //foreach (var w in i.qb03)
                            //                            for (int d = 0; d < i.qb03.Count(); d++)
                            //                            {
                            //                                //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                            //                                //////Console.WriteLine("w00~~~ " + d.ToString());
                            //                                if (checkList[a].wqb01 == d.ToString())
                            //                                {

                            //                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                    //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                            //                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                    //////Console.WriteLine("j~~ " + j);
                            //                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                    //ANS2 = Convert.ToString(qb03_count);
                            //                                    temp_j = i.qb03[d]; // 答案
                            //                                                        //////Console.WriteLine("jj~~ " + temp_j);
                            //                                }

                            //                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                            }
                            //                            // //////Console.WriteLine("cc~~~ " + p);
                            //                            //////Console.WriteLine("ANS2~~ " + ANS2);

                            //                            //temp_value = checkList[a].wqb99; // entry
                            //                        }
                            //                    }
                            //                }



                            //                bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                            //                                                             //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                            //                if (ischeck == true)
                            //                {
                            //                    if (j == "是" || j == "已發")
                            //                    {
                            //                        isRed = true;
                            //                        isGreen = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        isGreen = true;
                            //                        isRed = false;
                            //                    }
                            //                }
                            //                //////Console.WriteLine("j3~~~ " + j);
                            //                //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                            //                //////Console.WriteLine("isRed3~~~ " + isRed);
                            //                //////Console.WriteLine("isGreen3~~~ " + isGreen);

                            //                //////Console.WriteLine("TFcount~~~" + TFcount);
                            //                if (TFcount == 1)
                            //                {
                            //                    check_box = new CheckBox // 產生checkbox
                            //                    {

                            //                        IsChecked = ischeck,
                            //                        Margin = new Thickness(-5, 0, 0, 0),
                            //                        //Color = Color.FromHex("264653")
                            //                        Color = Color.Red,

                            //                    };
                            //                }
                            //                else
                            //                {
                            //                    check_box = new CheckBox // 產生checkbox
                            //                    {

                            //                        IsChecked = ischeck,
                            //                        Margin = new Thickness(-5, 0, 0, 0),
                            //                        //Color = Color.FromHex("264653")
                            //                        Color = Color.Green
                            //                    };
                            //                }



                            //                //if (j == "是")
                            //                //{
                            //                //entny = new Entry // 產生Entry
                            //                //{
                            //                //    Placeholder = "請說明",
                            //                //    Text = temp_value,
                            //                //    IsVisible = isEntry,
                            //                //    IsEnabled = isEntry


                            //                //};



                            //                //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                            //                //{
                            //                //    for (int a = 0; a < checkList.Count(); a++)
                            //                //    {
                            //                //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                //        {
                            //                //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                            //                //            {
                            //                //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                            //                //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                            //                //                checkList[a].wqb99 = ee.NewTextValue;
                            //                //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                            //                //            }

                            //                //        }

                            //                //    }
                            //                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //                //    //{
                            //                //    //    //////Console.WriteLine("IMMMM~~~~");
                            //                //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //                //    //    {
                            //                //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                            //                //    //        foreach (var TempAnsList in c)
                            //                //    //        {
                            //                //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                            //                //    //            //////Console.WriteLine("WHO~~" + who);
                            //                //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                            //                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                //    //            {
                            //                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //                //    //                {
                            //                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //                //    //                    for (int d = 0; d < checkList.Count(); d++)
                            //                //    //                    {
                            //                //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                            //                //    //                        {
                            //                //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                            //                //    //                        }

                            //                //    //                    }

                            //                //    //                    //temp_j = TempAnsList.wqb01;
                            //                //    //                    //temp_value = TempAnsList.wqb99;
                            //                //    //                    //var check2 = new checkInfo
                            //                //    //                    //{
                            //                //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                            //                //    //                    //    wqb99 = TempAnsList.wqb99

                            //                //    //                    //};

                            //                //    //                    //checkList.Add(check2);

                            //                //    //                    //MapView.AccDatabase.DeleteItem(b);
                            //                //    //                }
                            //                //    //            }


                            //                //    //        }
                            //                //    //    }



                            //                //    //}
                            //                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //                //    //{
                            //                //    //    //////Console.WriteLine("IMMMM~~~~");

                            //                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //                //    //    {
                            //                //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                            //                //    //        foreach (var TempAnsList in a)
                            //                //    //        {
                            //                //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                            //                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                //    //            {
                            //                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //                //    //                {
                            //                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //                //    //                    //temp_j = TempAnsList.wqb01;
                            //                //    //                    for (int c = 0; c < checkList.Count(); c++)
                            //                //    //                    {
                            //                //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                            //                //    //                        {
                            //                //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                            //                //    //                        }
                            //                //    //                    }
                            //                //    //                }
                            //                //    //            }

                            //                //    //        }
                            //                //    //    }


                            //                //    //}
                            //                //};
                            //                //}
                            //                check_box.CheckedChanged += async (s, e) =>
                            //                {
                            //                    //////Console.WriteLine("checkboxin3~~~");
                            //                    if (e.Value) // 如果選是，要跳出entry所以需要reset
                            //                    {
                            //                        ////////Console.WriteLine("IN~~~");
                            //                        //ischeck = true;
                            //                        //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                            //                        for (int a = 0; a < checkList.Count(); a++)
                            //                        {
                            //                            if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                            {
                            //                                if (checkList[a].qb_s_num == i.qb_s_num)
                            //                                {
                            //                                    checkList.RemoveAt(a);
                            //                                    //checkList2.RemoveAt(a);
                            //                                }
                            //                            }

                            //                        }
                            //                        ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                        //if (tmp_name_list.Contains(questionList.ClientName))
                            //                        //{
                            //                        //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                        //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                            //                        //    //////Console.WriteLine("a~ " + total);
                            //                        //    tmp_name_list.Remove(questionList.ClientName);
                            //                        //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                            //                        //    //////Console.WriteLine("b~ " + total2);
                            //                        //}
                            //                        //////Console.WriteLine("jjj~~~ " + j);
                            //                        if (j == "未發")
                            //                        {
                            //                            //IsChoose = true;
                            //                            CheckboxList[questionList.ClientName + questionList.qb_s_num] = true;
                            //                            ChooseSaveToDB(questionList.ClientName, true);
                            //                            //////Console.WriteLine("LLL~~~ " + IsChoose);
                            //                        }
                            //                        //if (j == "是" || j == "已發")
                            //                        //{
                            //                        //    ANS = 0;
                            //                        //    ANS2 = Convert.ToString(ANS);


                            //                        //}
                            //                        //else
                            //                        //{
                            //                        //    ANS = 1;
                            //                        //    ANS2 = Convert.ToString(ANS);

                            //                        //}
                            //                        for (int d = 0; d < i.qb03.Count(); d++)
                            //                        {
                            //                            //Console.WriteLine("j00~~ " + j);
                            //                            //Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                            if (j == i.qb03[d])
                            //                            {

                            //                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                                //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                //////Console.WriteLine("j~~ " + j);
                            //                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                //ANS2 = Convert.ToString(qb03_count);
                            //                                ANS2 = d.ToString();
                            //                                //////Console.WriteLine("jj~~ " + temp_j);
                            //                                //////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                            }

                            //                            //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                        }
                            //                        // //////Console.WriteLine("cc~~~ " + p);
                            //                        //////Console.WriteLine("ANS2~~ " + ANS2);
                            //                        //////Console.WriteLine("cc~~~ " + p);
                            //                        //////Console.WriteLine("ANS21~~~ " + ANS2);
                            //                        // 把問題選項存進資料庫
                            //                        ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                            //                        //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                            //                        //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                            //                        ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                            //                        //Console.WriteLine("j~~" + j);
                            //                        if (j == "是" || j == "已發")
                            //                        {
                            //                            //Console.WriteLine("G_in~~~ ");
                            //                            color = "Green";
                            //                        }
                            //                        else
                            //                        {
                            //                            //Console.WriteLine("R_in~~~ ");
                            //                            color = "Red";
                            //                        }
                            //                        //////Console.WriteLine("color~~~ " + color);
                            //                        QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                            //                        ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                            //                        var check = new checkInfo
                            //                        {
                            //                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                            wqb01 = ANS2 // 答案

                            //                        };
                            //                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                        var check3 = new checkInfo
                            //                        {
                            //                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                            qb_order = i.qb_order,
                            //                            wqb01 = ANS2 // 答案

                            //                        };
                            //                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                        checkList2.Add(check3); // for save
                            //                                                //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //                        checkList.Add(check); // for check
                            //                                              //checkList2.Add(check3); // for save
                            //                                              ////////Console.WriteLine("CHECK" + checkList[0]);
                            //                        ///
                            //                        reset();
                            //                        //isReset = true;
                            //                        //if (j == "未發")
                            //                        //{
                            //                        //    reset();
                            //                        //}


                            //                        // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                            //                    }
                            //                    else // 選否則不用reset
                            //                    {
                            //                        //ischeck = false;
                            //                        //////Console.WriteLine("remove~~~~~");
                            //                        for (int a = 0; a < checkList.Count(); a++)
                            //                        {
                            //                            if (checkList[a].qb_s_num == i.qb_s_num)
                            //                            {
                            //                                checkList.RemoveAt(a);
                            //                            }
                            //                        }
                            //                    }

                            //                    //foreach (var b in checkList)
                            //                    //{
                            //                    //    //////Console.WriteLine("HERE~~");
                            //                    //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                            //                    //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                            //                    //    //////Console.WriteLine("qb03 : " + b.wqb01);
                            //                    //    //////Console.WriteLine("enrty : " + b.wqb99);
                            //                    //}
                            //                };
                            //                //////Console.WriteLine("isReset~~~~ " + isReset);
                            //                //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                            //                //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                            //                //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                            //                //////Console.WriteLine("WWWRRR_j~~~ " + j);
                            //                ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                            //                if (isReset == true || isDB == true)
                            //                {
                            //                    if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                    {
                            //                        //Console.WriteLine("JKL1~~~ " + j);
                            //                        if (j == "是")
                            //                        {
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Green,
                            //                                //Margin = new Thickness(-10,0,0,0),
                            //                                FontSize = 20
                            //                            };
                            //                            isRed = false;
                            //                            //isReset = false;
                            //                        }
                            //                        else
                            //                        {
                            //                            //////Console.WriteLine("JKL1-1~~~ " + j);
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Black,
                            //                                FontSize = 20
                            //                            };
                            //                        }

                            //                    }
                            //                    else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                    {
                            //                        //////Console.WriteLine("JKL2~~~ " + j);
                            //                        if (j == "否")
                            //                        {
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Red,
                            //                                //Margin = new Thickness(-10, 0, 0, 0),
                            //                                FontSize = 20
                            //                            };
                            //                            isGreen = false;
                            //                            //isReset = false;
                            //                        }
                            //                        else
                            //                        {
                            //                            //////Console.WriteLine("JKL2-1~~~ " + j);
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Black,
                            //                                FontSize = 20
                            //                            };
                            //                        }

                            //                    }
                            //                    else
                            //                    {
                            //                        //////Console.WriteLine("JKL3~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    label_check = new Label // 選項
                            //                    {
                            //                        Text = j,
                            //                        TextColor = Color.Black,
                            //                        FontSize = 20
                            //                    };
                            //                }


                            //                var stack_check = new StackLayout // checkbox跟選項
                            //                {
                            //                    Orientation = StackOrientation.Horizontal,
                            //                    Children = { check_box, label_check }
                            //                };

                            //                //var ques_all_check = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Horizontal,
                            //                //    Children = { stack_check, stack }
                            //                //};

                            //                //var stack_enrty = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Vertical,
                            //                //    Children = { stack_check, entny }
                            //                //};
                            //                stack_ques.Children.Add(stack_check);
                            //                //count = count + 1;

                            //                //var final_stack = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Horizontal,
                            //                //    Children = { stack_ques, label_que_name }
                            //                //};
                            //                //}
                            //            }
                            //            else
                            //            {
                            //                //Console.WriteLine("qborder_2_in~~~ ");
                            //                //Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                //Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                            //                //Console.WriteLine("name~~ " + questionList.ClientName);
                            //                //////Console.WriteLine("CVB~~~~");
                            //                //foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                            //                //{
                            //                //count = 0;
                            //                ////Console.WriteLine("nameCC~~~ " + questionList.ClientName);
                            //                ////Console.WriteLine("countCC~~~ " + questionList.qbs.Count());
                            //                ////Console.WriteLine("order~~~ " + i.qb_order);
                            //                ////Console.WriteLine("result_num~~~ " + result_num);
                            //                TFcount = TFcount + 1;
                            //                var temp_j = "";
                            //                var temp_value = "";
                            //                //var temp_j_map = "";
                            //                //var temp_value_map = "";

                            //                // 跑選是的reset把checkList抓回來判斷
                            //                if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                            //                {
                            //                    //////Console.WriteLine("third~~ ");
                            //                    //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                            //                    //////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                    var _wqhsnum = questionList.wqh_s_num;
                            //                    temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                            //                    //////Console.WriteLine("tempj~~ " + temp_j);
                            //                    for (int d = 0; d < i.qb03.Count(); d++)
                            //                    {
                            //                        //////Console.WriteLine("j00~~ " + j);
                            //                        //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                        if (temp_j == i.qb03[d])
                            //                        {

                            //                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                            //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                            //////Console.WriteLine("j~~ " + j);
                            //                            //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                            //ANS2 = Convert.ToString(qb03_count);
                            //                            ANS2 = d.ToString();
                            //                            //////Console.WriteLine("jj~~ " + temp_j);
                            //                            //////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                        }

                            //                        //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                    }
                            //                    //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                            //                    //////Console.WriteLine("qborder~~~ " + i.qb_order);
                            //                    //////Console.WriteLine("why~~ " + TmpAdd_elseList[questionList.wqh_s_num + i.qb_order]);
                            //                    checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                    var check3 = new checkInfo
                            //                    {
                            //                        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                        qb_order = i.qb_order,
                            //                        wqb01 = ANS2 // 答案

                            //                    };
                            //                    ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    checkList2.Add(check3); // for save
                            //                                            //if (TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] == false)
                            //                                            //{
                            //                                            //    //////Console.WriteLine("ans2~~~ " + ANS2);
                            //                                            //    var check3 = new checkInfo
                            //                                            //    {
                            //                                            //        wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                                            //        qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                                            //        qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                                            //        qb_order = i.qb_order,
                            //                                            //        wqb01 = ANS2 // 答案

                            //                    //    };
                            //                    //    //////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //    checkList2.Add(check3); // for save
                            //                    //    //////Console.WriteLine("count12~~ " + checkList2.Count());
                            //                    //    Add_elseSaveToDB(questionList.wqh_s_num, i.qb_order);
                            //                    //    TmpAdd_elseList[questionList.wqh_s_num + i.qb_order] = true;
                            //                    //    //////Console.WriteLine("checkList2Add99~~~ ");
                            //                    //}

                            //                    // //////Console.WriteLine("cc~~~ " + p);
                            //                    //////Console.WriteLine("ANS2~~ " + ANS2);
                            //                    //var ques = questionList.wqh_s_num + i.qb_order;
                            //                    ////////Console.WriteLine("ques!!! " + ques);
                            //                    //for(int e = 0; e < checkList2.Count(); e++)
                            //                    //{
                            //                    //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                            //                    //    //////Console.WriteLine("check!!! " + check);
                            //                    //    if (check != ques)
                            //                    //    {
                            //                    //        //////Console.WriteLine("checkin!!! " + check);
                            //                    //        //////Console.WriteLine("quesin!!! " + ques);
                            //                    //        var check3 = new checkInfo
                            //                    //        {
                            //                    //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //            qb_order = i.qb_order,
                            //                    //            wqb01 = ANS2 // 答案

                            //                    //        };
                            //                    //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                    //        checkList2.Add(check3); // for save
                            //                    //        //////Console.WriteLine("checkList2Add2~~~ ");
                            //                    //    }
                            //                    //}
                            //                    //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                            //                    //qb03_count = 0;
                            //                    //foreach (var y in i.qb03)
                            //                    //{
                            //                    //    //////Console.WriteLine("yAA~~~ " + y);
                            //                    //    if (temp_j == y)
                            //                    //    {
                            //                    //        //////Console.WriteLine("temp_j~~ " + temp_j);
                            //                    //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                            //                    //        p = Convert.ToString(qb03_count);
                            //                    //    }
                            //                    //    qb03_count = qb03_count + 1;

                            //                    //}
                            //                    ////////Console.WriteLine("cc~~~ " + p);
                            //                    ////temp_value = TempAnsList.wqb99; // 之前的entry
                            //                    //var check2 = new checkInfo
                            //                    //{
                            //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                    //    wqb01 = p,// 答案
                            //                    //              //wqb99 = TempAnsList.wqb99

                            //                    //};
                            //                }
                            //                // 跑選是的reset把checkList抓回來判斷
                            //                //////Console.WriteLine("checklist2~count3~ " + checkList2.Count());
                            //                for (int a = 0; a < checkList.Count(); a++)
                            //                {
                            //                    //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                            //                    //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                            //                    ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                    if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                    {
                            //                        ////////Console.WriteLine("IMMMM222~~~~");
                            //                        //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                            //                        if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                            //                        {
                            //                            //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                            //                            //foreach (var w in i.qb03)
                            //                            for (int d = 0; d < i.qb03.Count(); d++)
                            //                            {
                            //                                //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                            //                                //////Console.WriteLine("w00~~~ " + d.ToString());
                            //                                if (checkList[a].wqb01 == d.ToString())
                            //                                {

                            //                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                    //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                            //                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                    //////Console.WriteLine("j~~ " + j);
                            //                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                    //ANS2 = Convert.ToString(qb03_count);
                            //                                    temp_j = i.qb03[d]; // 答案
                            //                                                        //////Console.WriteLine("jj~~ " + temp_j);
                            //                                }

                            //                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                            }
                            //                            // //////Console.WriteLine("cc~~~ " + p);
                            //                            //////Console.WriteLine("ANS2~~ " + ANS2);

                            //                            //temp_value = checkList[a].wqb99; // entry
                            //                        }
                            //                    }
                            //                }



                            //                bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                            //                                                             //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                            //                if (ischeck == true)
                            //                {
                            //                    if (j == "是" || j == "已發")
                            //                    {
                            //                        isRed = true;
                            //                        isGreen = false;
                            //                    }
                            //                    else
                            //                    {
                            //                        isGreen = true;
                            //                        isRed = false;
                            //                    }
                            //                }
                            //                //////Console.WriteLine("j3~~~ " + j);
                            //                //////Console.WriteLine("isckeck3~~~~ " + ischeck);
                            //                //////Console.WriteLine("isRed3~~~ " + isRed);
                            //                //////Console.WriteLine("isGreen3~~~ " + isGreen);

                            //                //////Console.WriteLine("TFcount~~~" + TFcount);
                            //                if (TFcount == 1)
                            //                {
                            //                    check_box = new CheckBox // 產生checkbox
                            //                    {

                            //                        IsChecked = ischeck,
                            //                        Margin = new Thickness(-5, 0, 0, 0),
                            //                        //Color = Color.FromHex("264653")
                            //                        Color = Color.Red,

                            //                    };
                            //                }
                            //                else
                            //                {
                            //                    check_box = new CheckBox // 產生checkbox
                            //                    {

                            //                        IsChecked = ischeck,
                            //                        Margin = new Thickness(-5, 0, 0, 0),
                            //                        //Color = Color.FromHex("264653")
                            //                        Color = Color.Green
                            //                    };
                            //                }



                            //                //if (j == "是")
                            //                //{
                            //                //entny = new Entry // 產生Entry
                            //                //{
                            //                //    Placeholder = "請說明",
                            //                //    Text = temp_value,
                            //                //    IsVisible = isEntry,
                            //                //    IsEnabled = isEntry


                            //                //};



                            //                //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                            //                //{
                            //                //    for (int a = 0; a < checkList.Count(); a++)
                            //                //    {
                            //                //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                //        {
                            //                //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                            //                //            {
                            //                //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                            //                //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                            //                //                checkList[a].wqb99 = ee.NewTextValue;
                            //                //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                            //                //            }

                            //                //        }

                            //                //    }
                            //                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //                //    //{
                            //                //    //    //////Console.WriteLine("IMMMM~~~~");
                            //                //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //                //    //    {
                            //                //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                            //                //    //        foreach (var TempAnsList in c)
                            //                //    //        {
                            //                //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                            //                //    //            //////Console.WriteLine("WHO~~" + who);
                            //                //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                            //                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                //    //            {
                            //                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //                //    //                {
                            //                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //                //    //                    for (int d = 0; d < checkList.Count(); d++)
                            //                //    //                    {
                            //                //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                            //                //    //                        {
                            //                //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                            //                //    //                        }

                            //                //    //                    }

                            //                //    //                    //temp_j = TempAnsList.wqb01;
                            //                //    //                    //temp_value = TempAnsList.wqb99;
                            //                //    //                    //var check2 = new checkInfo
                            //                //    //                    //{
                            //                //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                            //                //    //                    //    wqb99 = TempAnsList.wqb99

                            //                //    //                    //};

                            //                //    //                    //checkList.Add(check2);

                            //                //    //                    //MapView.AccDatabase.DeleteItem(b);
                            //                //    //                }
                            //                //    //            }


                            //                //    //        }
                            //                //    //    }



                            //                //    //}
                            //                //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                            //                //    //{
                            //                //    //    //////Console.WriteLine("IMMMM~~~~");

                            //                //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                            //                //    //    {
                            //                //    //        var a = MapView.AccDatabase.GetAccountAsync(b);
                            //                //    //        foreach (var TempAnsList in a)
                            //                //    //        {
                            //                //    //            //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                            //                //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                            //                //    //            {
                            //                //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                            //                //    //                {
                            //                //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                            //                //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                            //                //    //                    //temp_j = TempAnsList.wqb01;
                            //                //    //                    for (int c = 0; c < checkList.Count(); c++)
                            //                //    //                    {
                            //                //    //                        if (checkList[c].qb_s_num == i.qb_s_num) // 第幾題
                            //                //    //                        {
                            //                //    //                            checkList[c].wqb99 = TempAnsList.wqb99;
                            //                //    //                        }
                            //                //    //                    }
                            //                //    //                }
                            //                //    //            }

                            //                //    //        }
                            //                //    //    }


                            //                //    //}
                            //                //};
                            //                //}
                            //                check_box.CheckedChanged += async (s, e) =>
                            //                {
                            //                    //////Console.WriteLine("checkboxin3~~~");
                            //                    if (e.Value) // 如果選是，要跳出entry所以需要reset
                            //                    {
                            //                        ////////Console.WriteLine("IN~~~");
                            //                        //ischeck = true;
                            //                        //IsResetList[questionList.wqh_s_num + i.qb_order] = true;

                            //                        for (int a = 0; a < checkList.Count(); a++)
                            //                        {
                            //                            if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //                            {
                            //                                if (checkList[a].qb_s_num == i.qb_s_num)
                            //                                {
                            //                                    checkList.RemoveAt(a);
                            //                                    //checkList2.RemoveAt(a);
                            //                                }
                            //                            }

                            //                        }
                            //                        ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                        //if (tmp_name_list.Contains(questionList.ClientName))
                            //                        //{
                            //                        //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                            //                        //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                            //                        //    //////Console.WriteLine("a~ " + total);
                            //                        //    tmp_name_list.Remove(questionList.ClientName);
                            //                        //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                            //                        //    //////Console.WriteLine("b~ " + total2);
                            //                        //}
                            //                        //////Console.WriteLine("jjj~~~ " + j);
                            //                        if (j == "未發")
                            //                        {
                            //                            //IsChoose = true;
                            //                            CheckboxList[questionList.ClientName + questionList.qb_s_num] = true;
                            //                            ChooseSaveToDB(questionList.ClientName, true);
                            //                            //////Console.WriteLine("LLL~~~ " + IsChoose);
                            //                        }
                            //                        //if (j == "是" || j == "已發")
                            //                        //{
                            //                        //    ANS = 0;
                            //                        //    ANS2 = Convert.ToString(ANS);


                            //                        //}
                            //                        //else
                            //                        //{
                            //                        //    ANS = 1;
                            //                        //    ANS2 = Convert.ToString(ANS);

                            //                        //}
                            //                        for (int d = 0; d < i.qb03.Count(); d++)
                            //                        {
                            //                            //////Console.WriteLine("j00~~ " + j);
                            //                            //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                            //                            if (j == i.qb03[d])
                            //                            {

                            //                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                            //                                //////Console.WriteLine("qb0311~~ " + qb03_count);
                            //                                //////Console.WriteLine("j~~ " + j);
                            //                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                            //                                //ANS2 = Convert.ToString(qb03_count);
                            //                                ANS2 = d.ToString();
                            //                                //////Console.WriteLine("jj~~ " + temp_j);
                            //                                //////Console.WriteLine("ANS2_2~~ " + ANS2);
                            //                            }

                            //                            //////Console.WriteLine("qb0322~~ " + qb03_count);
                            //                        }
                            //                        // //////Console.WriteLine("cc~~~ " + p);
                            //                        //////Console.WriteLine("ANS2~~ " + ANS2);
                            //                        //////Console.WriteLine("cc~~~ " + p);
                            //                        //////Console.WriteLine("ANS21~~~ " + ANS2);
                            //                        // 把問題選項存進資料庫
                            //                        ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                            //                        //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                            //                        //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                            //                        ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                            //                        ////////Console.WriteLine("j~~" + j);
                            //                        if (j == "是" || j == "已發")
                            //                        {
                            //                            color = "Red";
                            //                        }
                            //                        else
                            //                        {
                            //                            color = "Green";
                            //                        }
                            //                        //////Console.WriteLine("color~~~ " + color);
                            //                        QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                            //                        ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                            //                        var check = new checkInfo
                            //                        {
                            //                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                            wqb01 = ANS2 // 答案

                            //                        };
                            //                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            //                        var check3 = new checkInfo
                            //                        {
                            //                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                            //                            qb_order = i.qb_order,
                            //                            wqb01 = ANS2 // 答案

                            //                        };
                            //                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                            //                        checkList2.Add(check3); // for save
                            //                                                //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //                        checkList.Add(check); // for check
                            //                                              //checkList2.Add(check3); // for save
                            //                                              ////////Console.WriteLine("CHECK" + checkList[0]);
                            //                        ///
                            //                        reset();
                            //                        //isReset = true;
                            //                        //if (j == "未發")
                            //                        //{
                            //                        //    reset();
                            //                        //}


                            //                        // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                            //                    }
                            //                    else // 選否則不用reset
                            //                    {
                            //                        //ischeck = false;
                            //                        //////Console.WriteLine("remove~~~~~");
                            //                        for (int a = 0; a < checkList.Count(); a++)
                            //                        {
                            //                            if (checkList[a].qb_s_num == i.qb_s_num)
                            //                            {
                            //                                checkList.RemoveAt(a);
                            //                            }
                            //                        }
                            //                    }

                            //                    //foreach (var b in checkList)
                            //                    //{
                            //                    //    //////Console.WriteLine("HERE~~");
                            //                    //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                            //                    //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                            //                    //    //////Console.WriteLine("qb03 : " + b.wqb01);
                            //                    //    //////Console.WriteLine("enrty : " + b.wqb99);
                            //                    //}
                            //                };
                            //                //////Console.WriteLine("isReset~~~~ " + isReset);
                            //                //////Console.WriteLine("AAA3~~~ " + questionList.wqh_s_num);
                            //                //////Console.WriteLine("BBB3~~~ " + i.qb_order);
                            //                //////Console.WriteLine("WWWRRR3~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                            //                //////Console.WriteLine("WWWRRR_j~~~ " + j);
                            //                ////////Console.WriteLine("WWW~~~ " + IsResetList[questionList.wqh_s_num + i.qb_order]);
                            //                if (isReset == true || isDB == true)
                            //                {
                            //                    if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                    {
                            //                        //////Console.WriteLine("JKL1~~~ " + j);
                            //                        if (j == "是" || j == "已發")
                            //                        {
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Red,
                            //                                //Margin = new Thickness(-10,0,0,0),
                            //                                FontSize = 20
                            //                            };
                            //                            isRed = false;
                            //                            //isReset = false;
                            //                        }
                            //                        else
                            //                        {
                            //                            //////Console.WriteLine("JKL1-1~~~ " + j);
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Black,
                            //                                FontSize = 20
                            //                            };
                            //                        }

                            //                    }
                            //                    else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                            //                    {
                            //                        //////Console.WriteLine("JKL2~~~ " + j);
                            //                        if (j == "未發" || j == "否")
                            //                        {
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Green,
                            //                                //Margin = new Thickness(-10, 0, 0, 0),
                            //                                FontSize = 20
                            //                            };
                            //                            isGreen = false;
                            //                            //isReset = false;
                            //                        }
                            //                        else
                            //                        {
                            //                            //////Console.WriteLine("JKL2-1~~~ " + j);
                            //                            label_check = new Label // 選項
                            //                            {
                            //                                Text = j,
                            //                                TextColor = Color.Black,
                            //                                FontSize = 20
                            //                            };
                            //                        }

                            //                    }
                            //                    else
                            //                    {
                            //                        //////Console.WriteLine("JKL3~~~ " + j);
                            //                        label_check = new Label // 選項
                            //                        {
                            //                            Text = j,
                            //                            TextColor = Color.Black,
                            //                            FontSize = 20
                            //                        };
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    label_check = new Label // 選項
                            //                    {
                            //                        Text = j,
                            //                        TextColor = Color.Black,
                            //                        FontSize = 20
                            //                    };
                            //                }


                            //                var stack_check = new StackLayout // checkbox跟選項
                            //                {
                            //                    Orientation = StackOrientation.Horizontal,
                            //                    Children = { check_box, label_check }
                            //                };

                            //                //var ques_all_check = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Horizontal,
                            //                //    Children = { stack_check, stack }
                            //                //};

                            //                //var stack_enrty = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Vertical,
                            //                //    Children = { stack_check, entny }
                            //                //};
                            //                stack_ques.Children.Add(stack_check);
                            //                //count = count + 1;

                            //                //var final_stack = new StackLayout
                            //                //{
                            //                //    Orientation = StackOrientation.Horizontal,
                            //                //    Children = { stack_ques, label_que_name }
                            //                //};
                            //                //}
                            //            }

                            //        }
                            //    }
                            //}
                            //else
                            //{
                                foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                                {
                                    //count = 0;
                                    TFcount = TFcount + 1;
                                    var temp_j = "";
                                    var temp_value = "";
                                    //var temp_j_map = "";
                                    //var temp_value_map = "";
                                    if (TmpAnsList.ContainsKey(questionList.ClientName + i.qb_order) && TmpAnsList[questionList.ClientName + i.qb_order] != "")
                                    {
                                        //////Console.WriteLine("first~~ ");
                                        //////Console.WriteLine("wqh2222~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        var _wqhsnum = questionList.wqh_s_num;
                                        temp_j = TmpAnsList[questionList.ClientName + i.qb_order];
                                        //////Console.WriteLine("tempj~~ " + temp_j);
                                        for (int d = 0; d < i.qb03.Count(); d++)
                                        {
                                            //////Console.WriteLine("j00~~ " + j);
                                            //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                            if (temp_j == i.qb03[d])
                                            {

                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                //////Console.WriteLine("j~~ " + j);
                                                //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                //ANS2 = Convert.ToString(qb03_count);
                                                ANS2 = d.ToString();
                                                //////Console.WriteLine("jj~~ " + temp_j);
                                                //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                            }

                                            //////Console.WriteLine("qb0322~~ " + qb03_count);
                                        }
                                        //////Console.WriteLine("wqh3333~~ " + questionList.wqh_s_num);
                                        //////Console.WriteLine("qborder~~~ " + i.qb_order);
                                        //////Console.WriteLine("why~~ " + TmpAddList[questionList.wqh_s_num + i.qb_order]);
                                        //if (TmpAddList[questionList.wqh_s_num + i.qb_order] == false)
                                        //{

                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = ANS2 // 答案

                                        };
                                        ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                        checkList2.Add(check3); // for save
                                        AddSaveToDB(questionList.wqh_s_num, i.qb_order);
                                        TmpAddList[questionList.wqh_s_num + i.qb_order] = true;
                                        //////Console.WriteLine("checkList2Add2~~~ ");
                                        //}

                                        // //////Console.WriteLine("cc~~~ " + p);
                                        //////Console.WriteLine("ANS2~~ " + ANS2);
                                        //var ques = questionList.wqh_s_num + i.qb_order;
                                        ////////Console.WriteLine("ques!!! " + ques);
                                        //for(int e = 0; e < checkList2.Count(); e++)
                                        //{
                                        //    var check = checkList2[e].wqh_s_num + checkList2[e].qb_order;
                                        //    //////Console.WriteLine("check!!! " + check);
                                        //    if (check != ques)
                                        //    {
                                        //        //////Console.WriteLine("checkin!!! " + check);
                                        //        //////Console.WriteLine("quesin!!! " + ques);
                                        //        var check3 = new checkInfo
                                        //        {
                                        //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //            qb_order = i.qb_order,
                                        //            wqb01 = ANS2 // 答案

                                        //        };
                                        //        //////Console.WriteLine("count1~~ " + checkList2.Count());
                                        //        checkList2.Add(check3); // for save
                                        //        //////Console.WriteLine("checkList2Add2~~~ ");
                                        //    }
                                        //}
                                        //////Console.WriteLine("checklist2~count2~ " + checkList2.Count());
                                        //qb03_count = 0;
                                        //foreach (var y in i.qb03)
                                        //{
                                        //    //////Console.WriteLine("yAA~~~ " + y);
                                        //    if (temp_j == y)
                                        //    {
                                        //        //////Console.WriteLine("temp_j~~ " + temp_j);
                                        //        //////Console.WriteLine("qb03_count~~ " + qb03_count);
                                        //        p = Convert.ToString(qb03_count);
                                        //    }
                                        //    qb03_count = qb03_count + 1;

                                        //}
                                        ////////Console.WriteLine("cc~~~ " + p);
                                        ////temp_value = TempAnsList.wqb99; // 之前的entry
                                        //var check2 = new checkInfo
                                        //{
                                        //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                        //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                        //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                        //    wqb01 = p,// 答案
                                        //              //wqb99 = TempAnsList.wqb99

                                        //};
                                    }
                                    // 跑選是的reset把checkList抓回來判斷
                                    for (int a = 0; a < checkList.Count(); a++)
                                    {
                                        //////Console.WriteLine("check11~~ " + checkList[a].wqh_s_num);
                                        //////Console.WriteLine("ques11~~~ " + questionList.wqh_s_num);
                                        ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                                        {
                                            ////////Console.WriteLine("IMMMM222~~~~");
                                            //////Console.WriteLine("AAQ~~~ " + questionList.wqh_s_num);
                                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                                            {
                                                //////Console.WriteLine("BBQ~~~~ " + i.qb_s_num);

                                                //foreach (var w in i.qb03)
                                                for (int d = 0; d < i.qb03.Count(); d++)
                                                {
                                                    //////Console.WriteLine("check00~~ " + checkList[a].wqb01);
                                                    //////Console.WriteLine("w00~~~ " + d.ToString());
                                                    if (checkList[a].wqb01 == d.ToString())
                                                    {

                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //////Console.WriteLine("check~~ " + checkList[a].wqb01);
                                                        //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                        //////Console.WriteLine("j~~ " + j);
                                                        //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                        //ANS2 = Convert.ToString(qb03_count);
                                                        temp_j = i.qb03[d]; // 答案
                                                                            //////Console.WriteLine("jj~~ " + temp_j);
                                                    }

                                                    //////Console.WriteLine("qb0322~~ " + qb03_count);
                                                }
                                                // //////Console.WriteLine("cc~~~ " + p);
                                                //////Console.WriteLine("ANS2~~ " + ANS2);

                                                //temp_value = checkList[a].wqb99; // entry
                                            }
                                        }
                                    }

                                    bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                                                                                 //bool isMoreCheckbox = (temp_j == "未發") ? true : false; // 如果答案是 未發 -> 第四題顯示
                                    if (ischeck == true)
                                    {
                                        if (j == "是" || j == "已發")
                                        {
                                            isRed = true;
                                        }
                                        else
                                        {
                                            isGreen = true;
                                        }
                                    }
                                    //////Console.WriteLine("j5~~~ " + j);
                                    //////Console.WriteLine("isckeck5~~~~ " + ischeck);
                                    //////Console.WriteLine("isRed5~~~ " + isRed);
                                    //////Console.WriteLine("isGreen5~~~ " + isGreen);

                                    //////Console.WriteLine("TFcount~~~" + TFcount);
                                    //if (TFcount == 1)
                                    if (TFcount == 1)
                                    {
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(0, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Green
                                        };
                                    }
                                    else
                                    {
                                        check_box = new CheckBox // 產生checkbox
                                        {

                                            IsChecked = ischeck,
                                            Margin = new Thickness(-5, 0, 0, 0),
                                            //Color = Color.FromHex("264653")
                                            Color = Color.Red
                                        };
                                    }
                                    check_box.CheckedChanged += async (s, e) =>
                                    {
                                        //////Console.WriteLine("checkboxin4~~~");
                                        if (e.Value) // 如果選是，要跳出entry所以需要reset
                                        {
                                            ////////Console.WriteLine("IN~~~");
                                            //ischeck = true;
                                            //IsResetList[questionList.wqh_s_num + i.qb_order] = true;
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                                                {
                                                    if (checkList[a].qb_s_num == i.qb_s_num)
                                                    {
                                                        checkList.RemoveAt(a);
                                                        //checkList2.RemoveAt(a);
                                                    }
                                                }

                                            }
                                            ////////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //if (tmp_name_list.Contains(questionList.ClientName))
                                            //{
                                            //    //////Console.WriteLine("NAME~~~~" + questionList.ClientName);
                                            //    var total = tmp_name_list.Count(b => b == questionList.ClientName);
                                            //    //////Console.WriteLine("a~ " + total);
                                            //    tmp_name_list.Remove(questionList.ClientName);
                                            //    var total2 = tmp_name_list.Count(a => a == questionList.ClientName);
                                            //    //////Console.WriteLine("b~ " + total2);
                                            //}
                                            //////Console.WriteLine("rrr~~~ " + j);
                                            if (j == "未發")
                                            {
                                                IsChoose = true;
                                                ChooseSaveToDB(questionList.ClientName, true);
                                                //////Console.WriteLine("choose_la~~~~ " + IsChoose);
                                            }
                                            if (j == "其他")
                                            {
                                                EntrySaveToDB(questionList.ClientName, questionList.wqh_s_num, questionList.qb_s_num, i.qb_order);
                                                //////Console.WriteLine("saveentry~~~ ");
                                                //////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                                                //////Console.WriteLine("order~~ " + i.qb_order);
                                            }
                                            //for()
                                            for (int d = 0; d < i.qb03.Count(); d++)
                                            {
                                                //////Console.WriteLine("j00~~ " + j);
                                                //////Console.WriteLine("w00~~~ " + i.qb03[d]);
                                                if (j == i.qb03[d])
                                                {

                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    ////////Console.WriteLine("check~~ " + checkList2[a].wqb01);
                                                    //////Console.WriteLine("qb0311~~ " + qb03_count);
                                                    //////Console.WriteLine("j~~ " + j);
                                                    //////Console.WriteLine("w~~~ " + i.qb03[d]);
                                                    //ANS2 = Convert.ToString(qb03_count);
                                                    ANS2 = d.ToString();
                                                    //////Console.WriteLine("jj~~ " + temp_j);
                                                    //////Console.WriteLine("ANS2_2~~ " + ANS2);
                                                }

                                                //////Console.WriteLine("qb0322~~ " + qb03_count);
                                            }
                                            // //////Console.WriteLine("cc~~~ " + p);
                                            //////Console.WriteLine("ANS2~~ " + ANS2);
                                            //else if (j == "缺少")
                                            //{
                                            //    ANS = 2;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else if(j == "其他")
                                            //{
                                            //    ANS = 3;
                                            //    ANS2 = Convert.ToString(ANS);
                                            //}
                                            //else
                                            //{
                                            //    if(j == "已發")
                                            //    {
                                            //        ANS = "Y";
                                            //    }
                                            //}

                                            // 把問題選項存進資料庫
                                            ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                                            //////Console.WriteLine("qh~s~num~~~ " + questionList.qh_s_num);
                                            //////Console.WriteLine("questionList.qh_s_num~~!!  " + questionList.qh_s_num);
                                            ////////Console.WriteLine("i.qb_s_num~~" + i.qb_s_num);
                                            ////////Console.WriteLine("j~~" + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                color = "Red";
                                            }
                                            else
                                            {
                                                color = "Green";
                                            }
                                            //////Console.WriteLine("name~~~ " + questionList.ClientName);
                                            //////Console.WriteLine("color~~~ " + color);
                                            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName, i.qb_order);
                                            ResetSaveToDB(questionList.wqh_s_num, i.qb_order, color);
                                            var check = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                wqb01 = j // 答案

                                            };
                                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                            var check3 = new checkInfo
                                            {
                                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                                qb_order = i.qb_order,
                                                wqb01 = ANS2 // 答案

                                            };
                                            ////////Console.WriteLine("count1~~ " + checkList2.Count());
                                            checkList2.Add(check3); // for save
                                                                    //////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                            checkList.Add(check); // for check
                                                                  //checkList2.Add(check3); // for save
                                                                  ////////Console.WriteLine("CHECK" + checkList[0]);
                                            isReset = true;
                                            //if(j == "其他")
                                            //{
                                            //    ////Console.WriteLine("reset~other~~ ");
                                            //    reset();
                                            //}
                                            reset();
                                            // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                                        }
                                        else // 選否則不用reset
                                        {
                                            //ischeck = false;
                                            //////Console.WriteLine("remove~~~~~");
                                            for (int a = 0; a < checkList.Count(); a++)
                                            {
                                                if (checkList[a].qb_s_num == i.qb_s_num)
                                                {
                                                    checkList.RemoveAt(a);
                                                }
                                            }
                                        }

                                        //foreach (var b in checkList)
                                        //{
                                        //    //////Console.WriteLine("HERE~~");
                                        //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                                        //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                                        //    //////Console.WriteLine("qb03 : " + b.wqb01);
                                        //    //////Console.WriteLine("enrty : " + b.wqb99);
                                        //}
                                    };

                                    if (isReset == true || isDB == true)
                                    {
                                        if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Red" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //////Console.WriteLine("JKL1~~~ " + j);
                                            if (j == "是" || j == "未發")
                                            {
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Red,
                                                    FontSize = 20
                                                };
                                                isRed = false;
                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL1-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }

                                        }
                                        else if (IsGreenOrRed[questionList.wqh_s_num + i.qb_order] == "Green" && IsResetList[questionList.wqh_s_num + i.qb_order] == true)
                                        {
                                            //////Console.WriteLine("JKL2~~~ " + j);
                                            if (j == "已發" || j == "否")
                                            {
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Green,
                                                    FontSize = 20
                                                };
                                                isGreen = false;
                                            }
                                            else
                                            {
                                                //////Console.WriteLine("JKL2-1~~~ " + j);
                                                label_check = new Label // 選項
                                                {
                                                    Text = j,
                                                    TextColor = Color.Black,
                                                    FontSize = 20
                                                };
                                            }

                                        }
                                        else
                                        {
                                            label_check = new Label // 選項
                                            {
                                                Text = j,
                                                TextColor = Color.Black,
                                                FontSize = 20
                                            };
                                        }
                                    }
                                    else
                                    {
                                        label_check = new Label // 選項
                                        {
                                            Text = j,
                                            TextColor = Color.Black,
                                            FontSize = 20
                                        };
                                    }
                                    var stack_check = new StackLayout // checkbox跟選項
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Children = { check_box, label_check },

                                    };

                                    //var ques_all_check = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_check, stack }
                                    //};

                                    //var stack_enrty = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Vertical,
                                    //    Children = { stack_check, entny }
                                    //};
                                    stack_ques.Children.Add(stack_check);


                                    //count = count + 1;

                                    //var final_stack = new StackLayout
                                    //{
                                    //    Orientation = StackOrientation.Horizontal,
                                    //    Children = { stack_ques, label_que_name }
                                    //};
                                }
                            //}
                           

                            quesStack.Children.Add(stack); // w

                            //quesStack.Children.Add(final_stack);
                            //quesStack.Children.Add(label_que_name);
                            //quesStack.Children.Add(stack_ques);
                            var final_stack = new StackLayout
                            {
                                Orientation = StackOrientation.Vertical,
                                Children = { label_que_name, stack_ques }
                            };


                            Frame frame = new Frame // frame包上面那個stacklayout
                            {
                                Padding = new Thickness(10, 5, 10, 5),
                                Margin = new Thickness(5, 0, 5, 0),
                                BackgroundColor = Color.FromHex("eddcd2"),
                                CornerRadius = 10,
                                HasShadow = false,
                                Content = final_stack
                            };
                            quesStack.Children.Add(frame);

                            //var last_stack = new StackLayout
                            //{
                            //    Orientation = StackOrientation.Vertical,
                            //    Children = { stack, frame }
                            //};

                            //ScrollView scrollView = new ScrollView { Content = quesStack };
                            //Content = scrollView;
                            //ScrollView scrollView = new ScrollView
                            //{
                            //    Content = quesStack

                            //};
                            //quesStack.Children.Add(scrollView);
                            //ScrollView scrollView = new ScrollView { Content = quesStack };
                        }

                    }
                }
                if (i.qb02 == "3") // 問題類型(3 問答題)
                {
                    var temp_value = "";

                    ////////Console.WriteLine("Entry~~~ " + EntryList[questionList.wqh_s_num + i.qb_order]);
                    for (int a = 0; a < checkList.Count(); a++)
                    {
                        ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                        if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                        {
                            //////Console.WriteLine("IMMMM222~~~~");
                            if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                            {
                                //temp_j = checkList[a].wqb01; // 答案
                                temp_value = checkList[a].wqb99; // entry
                            }
                        }
                    }
                    if (EntryDB.GetAccountAsync2().Count() > 0)
                    {
                        //////Console.WriteLine("entrya~~");
                        for (int e = 0; e < EntryDB.GetAccountAsync2().Count(); e++)
                        {
                            //////Console.WriteLine("entryb~~~ ");
                            var f = EntryDB.GetAccountAsync(e);
                            foreach (var TempEntryList in f)
                            {
                                //////Console.WriteLine("temp_wqh~~ " + TempEntryList.wqh_s_num);
                                //////Console.WriteLine("q_wqh~~ " + questionList.wqh_s_num);
                                //////Console.WriteLine("name~~ " + questionList.ClientName);
                                if (TempEntryList.ClientName == questionList.ClientName)
                                {
                                    if (TempEntryList.wqh_s_num == questionList.wqh_s_num)
                                    {
                                        //////Console.WriteLine("entryc~~ ");
                                        //////Console.WriteLine("temp_qb~~ " + TempEntryList.qb_order);
                                        //////Console.WriteLine("i_qb~~~ " + i.qb_order);
                                        if (TempEntryList.qb_order == "4")
                                        {
                                            //////Console.WriteLine("entry_true~~ ");
                                            //////Console.WriteLine("qborder~~ " + i.qb_order);
                                            EntryList[questionList.ClientName] = true;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (EntrytxtDB.GetAccountAsync2().Count() > 0)
                    {
                        //////Console.WriteLine("entrya~~");
                        for (int e = 0; e < EntrytxtDB.GetAccountAsync2().Count(); e++)
                        {
                            //////Console.WriteLine("entryb~~~ ");
                            var f = EntrytxtDB.GetAccountAsync(e);
                            foreach (var TempEntrytxtList in f)
                            {
                                if (TempEntrytxtList.ClientName == questionList.ClientName)
                                {
                                    if (TempEntrytxtList.wqh_s_num == questionList.wqh_s_num)
                                    {
                                        //temp_value = TempEntrytxtList.entrytxt;
                                        if (EntrytxtList.ContainsKey(questionList.wqh_s_num + i.qb_order))
                                        {
                                            EntrytxtList.Remove(questionList.wqh_s_num + i.qb_order);
                                            EntrytxtList[questionList.wqh_s_num + i.qb_order] = TempEntrytxtList.entrytxt;
                                        }
                                        else
                                        {
                                            EntrytxtList[questionList.wqh_s_num + i.qb_order] = TempEntrytxtList.entrytxt;
                                        }
                                        // EntrytxtList[questionList.ClientName] = TempEntrytxtList.entrytxt;
                                        //////Console.WriteLine("entrytxt11~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);
                                        checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                                        var check3 = new checkInfo
                                        {
                                            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                            qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                            qb_order = i.qb_order,
                                            wqb01 = EntrytxtList[questionList.wqh_s_num + i.qb_order]
                                        };
                                        //////Console.WriteLine("entrytxt33~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);

                                        checkList2.Add(check3); // for save
                                    }
                                }
                            }
                        }
                        //////Console.WriteLine("entrytxt22~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);

                    }

                    //////Console.WriteLine("entrytxt44~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);
                    if (EntryList[questionList.ClientName] == true)
                    {
                        var label_que_name = new Label // 問題題號+題目
                        {
                            Text = i.qb_order + " " + i.qb01,
                            FontSize = 20,
                            TextColor = Color.Black
                        };

                        var stack_ques = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal
                        };
                        //////Console.WriteLine("i.qb03~~~ " + i.qb03);
                        //////Console.WriteLine("i.qb_order~~~ " + i.qb_order);
                        //////Console.WriteLine("result~~~ " + result);
                        //////Console.WriteLine("TorF~~~" + result == "星期三");
                        //////Console.WriteLine("TorF2~~~~ " + result.Equals("星期三"));
                        //////Console.WriteLine("count~~~ " + questionList.qbs.Count());

                        entny = new Entry // 產生Entry
                        {
                            Placeholder = "請說明",
                            Text = EntrytxtList[questionList.wqh_s_num + i.qb_order],
                            //IsVisible = isEntry,
                            //IsEnabled = isEntry


                        };
                        entny.TextChanged += async (ss, ee) =>
                        {
                            ////Console.WriteLine("Text11~" + ee.NewTextValue);
                            if (EntrytxtList.ContainsKey(questionList.wqh_s_num + i.qb_order))
                            {
                                EntrytxtList.Remove(questionList.wqh_s_num + i.qb_order);
                                EntrytxtList[questionList.wqh_s_num + i.qb_order] = ee.NewTextValue;
                            }
                            else
                            {
                                EntrytxtList[questionList.wqh_s_num + i.qb_order] = ee.NewTextValue;
                            }
                            EntrytxtList[questionList.wqh_s_num + i.qb_order] = ee.NewTextValue;
                            //////Console.WriteLine("entrytxt22~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);
                            ////Console.WriteLine("wqh~~~ " + questionList.wqh_s_num);
                            ////Console.WriteLine("order~~ " + i.qb_order);
                            checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == i.qb_order);
                            var check3 = new checkInfo
                            {
                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                                qb_order = i.qb_order,
                                wqb01 = EntrytxtList[questionList.wqh_s_num + i.qb_order]
                            };
                            //////Console.WriteLine("entrytxt33~~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);

                            checkList2.Add(check3); // for save
                            //checkList2.Add(check3); // for save
                            EntrytxtSaveDB(questionList.ClientName, questionList.wqh_s_num, EntrytxtList[questionList.wqh_s_num + i.qb_order]);
                            //for (int a = 0; a < checkList.Count(); a++)
                            //{
                            //    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                            //    {
                            //        if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                            //        {
                            //            //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                            //            //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                            //            checkList[a].wqb99 = ee.NewTextValue;
                            //            entry_list.Add(ee.NewTextValue);

                            //            //////Console.WriteLine("Text~" + ee.NewTextValue);
                            //        }

                            //    }

                            //}
                            ////////Console.WriteLine("entry~~~LA~ " + entry_list[entry_list.Count() - 1]);

                            //////Console.WriteLine("ans1~~ " + EntrytxtList[questionList.wqh_s_num + i.qb_order]);
                            //////Console.WriteLine("qborder~~ " + i.qb_order);

                        };
                        

                        //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                        //{
                        //    for (int a = 0; a < checkList.Count(); a++)
                        //    {
                        //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                        //        {
                        //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                        //            {
                        //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                        //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                        //                checkList[a].wqb99 = ee.NewTextValue;
                        //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                        //            }

                        //        }

                        //    }
                        //    //if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                        //    //{
                        //    //    //////Console.WriteLine("IMMMM~~~~");
                        //    //    //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                        //    //    for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                        //    //    {
                        //    //        var c = MapView.AccDatabase.GetAccountAsync(b);


                        //    //        foreach (var TempAnsList in c)
                        //    //        {
                        //    //            string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                        //    //            //////Console.WriteLine("WHO~~" + who);
                        //    //            //////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                        //    //            if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                        //    //            {
                        //    //                if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                        //    //                {
                        //    //                    //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                        //    //                    //////Console.WriteLine("who " + TempAnsList.qh_s_num);
                        //    //                    for (int d = 0; d < checkList.Count(); d++)
                        //    //                    {
                        //    //                        if (checkList[d].qb_s_num == i.qb_s_num) // 第幾題
                        //    //                        {
                        //    //                            checkList[d].wqb99 = TempAnsList.wqb99;
                        //    //                        }

                        //    //                    }

                        //    //                    //temp_j = TempAnsList.wqb01;
                        //    //                    //temp_value = TempAnsList.wqb99;
                        //    //                    //var check2 = new checkInfo
                        //    //                    //{
                        //    //                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                        //    //                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                        //    //                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                        //    //                    //    wqb01 = TempAnsList.wqb01,// 答案
                        //    //                    //    wqb99 = TempAnsList.wqb99

                        //    //                    //};

                        //    //                    //checkList.Add(check2);

                        //    //                    //MapView.AccDatabase.DeleteItem(b);
                        //    //                }
                        //    //            }


                        //    //        }
                        //    //    }


                        quesStack.Children.Add(stack); // w

                        //quesStack.Children.Add(final_stack);
                        //quesStack.Children.Add(label_que_name);
                        //quesStack.Children.Add(stack_ques);
                        var final_stack = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { label_que_name, stack_ques }
                        };

                        var entry_stack = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Children = { final_stack, entny }
                        };


                        Frame frame = new Frame // frame包上面那個stacklayout
                        {
                            Padding = new Thickness(10, 5, 10, 5),
                            Margin = new Thickness(5, 0, 5, 0),
                            BackgroundColor = Color.FromHex("eddcd2"),
                            CornerRadius = 10,
                            HasShadow = false,
                            Content = entry_stack
                        };
                        quesStack.Children.Add(frame);

                        //var last_stack = new StackLayout
                        //{
                        //    Orientation = StackOrientation.Vertical,
                        //    Children = { stack, frame }
                        //};

                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                        //Content = scrollView;
                        //ScrollView scrollView = new ScrollView
                        //{
                        //    Content = quesStack

                        //};
                        //quesStack.Children.Add(scrollView);
                        //ScrollView scrollView = new ScrollView { Content = quesStack };
                    }
                }


                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


                // Qtype = "1";
                //string set = questionList.wqh_s_num + i.qb_s_num;
                //TmpCheckList[set] = false;

                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // 多選題(未完成)
                ////if (i.qb02 == "2") // 問題類型(2是多選題)
                ////{
                ////    Qtype = "2";
                ////    //string set = questionList.wqh_s_num + i.qb_s_num;
                ////    //TmpCheckList[set] = false;
                ////    var label_que_name = new Label // 問題題號+題目
                ////    {
                ////        Text = i.qb_order + " " + i.qb01,
                ////        FontSize = 20,
                ////        TextColor = Color.Black
                ////    };

                ////    var stack_ques = new StackLayout
                ////    {
                ////        Orientation = StackOrientation.Horizontal
                ////    };

                ////    foreach (var j in i.qb03) // 跑選項的for迴圈(for產生幾個checkbox) // j => checkbox的選項 
                ////    {
                ////        var temp_j = "";

                ////        //var temp_value = "";
                ////        //var temp_j_map = "";
                ////        //var temp_value_map = "";

                ////        // 跑選是的reset把checkList抓回來判斷
                ////        for (int a = 0; a < checkList.Count(); a++)
                ////        {
                ////            ////////Console.WriteLine("COUNT222~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                ////            if (checkList[a].wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                ////            {
                ////                //////Console.WriteLine("IMMMM222~~~~");
                ////                if (checkList[a].qb_s_num == i.qb_s_num) // 判斷哪一題
                ////                {
                ////                    temp_j = checkList[a].wqb01; // 答案
                ////                    //temp_value = checkList[a].wqb99; // entry
                ////                }
                ////            }
                ////        }

                ////        if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                ////        {
                ////            // //////Console.WriteLine("IMMMM~~~~");
                ////            // //////Console.WriteLine("pp~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                ////            for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                ////            {
                ////                var a = MapView.AccDatabase.GetAccountAsync(b);


                ////                foreach (var TempAnsList in a)
                ////                {
                ////                    string who = TempAnsList.wqh_s_num + TempAnsList.qb_s_num;
                ////                    ////////Console.WriteLine("WHO~~" + who);
                ////                    ////////Console.WriteLine("WHOTF~~" + TmpCheckList[who]);
                ////                    //if (tmpchecklist[who] == false)
                ////                    //{

                ////                    //}
                ////                    if (TempAnsList.wqh_s_num == questionList.wqh_s_num) // 判斷問卷編號
                ////                    {
                ////                        if (TempAnsList.qb_s_num == i.qb_s_num) // 判斷哪一提
                ////                        {
                ////                            //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                ////                            //////Console.WriteLine("who " + TempAnsList.qh_s_num);

                ////                            //TmpCheckList[who] = true;
                ////                            temp_j = TempAnsList.wqb01;
                ////                            //temp_value = TempAnsList.wqb99;
                ////                            var check2 = new checkInfo
                ////                            {
                ////                                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                ////                                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                ////                                qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                ////                                wqb01 = TempAnsList.wqb01,// 答案
                ////                                                          //wqb99 = TempAnsList.wqb99

                ////                            };
                ////                            // //////Console.WriteLine("name " + TempAnsList.wqh_s_num);
                ////                            // //////Console.WriteLine("number " + TempAnsList.qb_s_num);
                ////                            // //////Console.WriteLine("entrytxt " + TempAnsList.wqb99);
                ////                            // //////Console.WriteLine("ID" + TempAnsList.ID);
                ////                            checkList.Add(check2);
                ////                            //MapView.AccDatabase.DeleteItem(b);
                ////                        }
                ////                    }
                ////                    //TempAccount TempAnsList = MapView.AccDatabase.GetAccountAsync(b).FirstOrDefault();
                ////                    //var check2 = new checkInfo
                ////                    //{
                ////                    //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                ////                    //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                ////                    //    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                ////                    //    wqb01 = TempAnsList.wqb01,// 答案
                ////                    //    wqb99 = TempAnsList.wqb99

                ////                    //};
                ////                    //checkList.Add(check2);

                ////                }
                ////            }



                ////        }



                ////        bool ischeck = (temp_j == j) ? true : false; // 再把剛剛的答案抓回來判斷(如果是就把他勾起來)
                ////        bool isEntry = (temp_j == "是") ? true : false; // 如果答案是 是 -> entry顯示


                ////        var check_box = new CheckBox // 產生checkbox
                ////        {
                ////            IsChecked = ischeck,
                ////            Color = Color.FromHex("264653")
                ////        };
                ////        if (j == "是")
                ////        {
                ////            //entny = new Entry // 產生Entry
                ////            //{
                ////            //    Placeholder = "請說明",
                ////            //    Text = temp_value,
                ////            //    IsVisible = isEntry,
                ////            //    IsEnabled = isEntry


                ////            //};



                ////            //entny.TextChanged += async (ss, ee) =>  // 點擊Entry
                ////            //{
                ////            //    for (int a = 0; a < checkList.Count(); a++)
                ////            //    {
                ////            //        if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                ////            //        {
                ////            //            if (checkList[a].qb_s_num == i.qb_s_num) // 第幾題
                ////            //            {
                ////            //                //////Console.WriteLine("Whichques~" + checkList[a].wqh_s_num);
                ////            //                //////Console.WriteLine("Qbnum~" + i.qb_s_num);

                ////            //                checkList[a].wqb99 = ee.NewTextValue;
                ////            //                //////Console.WriteLine("Text~" + ee.NewTextValue);
                ////            //            }

                ////            //        }

                ////            //    }

                ////            //};
                ////        }
                ////        check_box.CheckedChanged += async (s, e) =>
                ////        {
                ////            if (e.Value) // 如果選是，要跳出entry所以需要reset
                ////            {
                ////                //////Console.WriteLine("IN~~~");
                ////                for (int a = 0; a < checkList.Count(); a++)
                ////                {
                ////                    if (checkList[a].wqh_s_num == questionList.wqh_s_num)
                ////                    {
                ////                        if (checkList[a].qb_s_num == i.qb_s_num)
                ////                        {

                ////                            checkList.RemoveAt(a);
                ////                            checkList2.RemoveAt(a);
                ////                        }
                ////                    }

                ////                }
                ////                //for(int b = 0; b < checkList2.Count(); b++)
                ////                //{
                ////                //    if(checkList2[b].wqh_s_num == questionList.wqh_s_num)
                ////                //    {
                ////                //        if(checkList2[b].qb_s_num == questionList.qb_s_num)
                ////                //        {
                ////                //            if(checkList2[b].wqb01 == j)
                ////                //            {
                ////                //                checkList2.RemoveAt(b);
                ////                //            }
                ////                //        }    
                ////                //    }
                ////                //}
                ////                for (int a = 0; a < i.qb03.Count(); a++)
                ////                {
                ////                    //////Console.WriteLine("PPP~~" + i.qb03[a]);
                ////                    //////Console.WriteLine("OOO~~" + j);
                ////                    if (String.Equals(j, i.qb03[a]) == true)
                ////                    {
                ////                        //////Console.WriteLine("final~~" + String.Equals(j, i.qb03[a]));
                ////                        ANS = a;
                ////                        ANS2 = Convert.ToString(ANS);
                ////                        // ANS3 = "," + ANS2 ;
                ////                        if (tmpans_list.Contains(ANS2) == false) 
                ////                        {
                ////                            tmpans_list.Add(ANS2);
                ////                        }

                ////                        // //////Console.WriteLine("ANS2~~~~" + ANS2);
                ////                        // //////Console.WriteLine("ANS3~~~~" + ANS3);
                ////                        //if(tmpans_list.Count() == 0)
                ////                        //{
                ////                        //    tmpans_list.Add(ANS2);
                ////                        //}
                ////                        //else
                ////                        //{
                ////                        //    tmpans_list.Add(ANS3);
                ////                        //}
                ////                        //////Console.WriteLine("ANS11~~~" + ANS);
                ////                    }
                ////                }
                ////                for (int r = 0; r < tmpans_list.Count(); r++)
                ////                {
                ////                    //////Console.WriteLine("tmpans_list~~~" + tmpans_list[r]);
                ////                }
                ////                anslist = string.Join(",", tmpans_list.ToArray());
                ////                //////Console.WriteLine("anslist~~~" + anslist);
                ////                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, i.qb_s_num, j, questionList.ClientName);

                ////                var check = new checkInfo
                ////                {
                ////                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                ////                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                ////                    qb_s_num = i.qb_s_num, // 問題編號(第幾題)
                ////                    wqb01 = j // 答案

                ////                };
                ////                var check3 = new checkInfo
                ////                {
                ////                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                ////                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                ////                    qb_s_num = i.qb_s_num,// 問題編號(第幾題)
                ///                     qb_order = i.qb_order,
                ////                    wqb01 = anslist // 答案

                ////                };
                ////                // //////Console.WriteLine("anslist~~~~" + anslist);
                ////                //checkListtmp.Add(check);
                ////                checkList.Add(check);
                ////                checkList2.Add(check3);

                ////                ////////Console.WriteLine("ANS22~~~" + ANS);
                ////                //var check = new checkInfo
                ////                //{
                ////                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                ////                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                ////                //    qb_s_num = i.qb_s_num,// 問題編號(第幾題)
                ////                //    wqb01 = ANS2 // 答案

                ////                //};
                ////                ////////Console.WriteLine("ans~~~~" + j);
                ////                //checkList2.Add(check);



                ////                ////////Console.WriteLine("CHECK" + checkList[0]);
                ////                //reset();
                ////                // 因為+entry之前畫面已run好，所以要+entry要重run一次再把選項抓回來填進去


                ////            }
                ////            else // 選否則不用reset
                ////            {
                ////                for (int b = 0; b < checkList.Count(); b++)
                ////                {
                ////                    if (checkList[b].qb_s_num == i.qb_s_num)
                ////                    {

                ////                        for (int a = 0; a < i.qb03.Count(); a++)
                ////                        {
                ////                            //////Console.WriteLine("PPP~~" + i.qb03[a]);
                ////                            //////Console.WriteLine("OOO~~" + j);
                ////                            if (String.Equals(checkList[b].wqb01[a], i.qb03[a]) == true)
                ////                            {
                ////                                checkList.RemoveAt(checkList[b].wqb01[a]);
                ////                                //////Console.WriteLine("ans~~~" + checkList[b].wqb01[a]);

                ////                            }
                ////                        }

                ////                    }
                ////                }

                ////            }

                ////            //foreach (var b in checkList2)
                ////            //{
                ////            //    //////Console.WriteLine("HERE~~");
                ////            //    //////Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                ////            //    //////Console.WriteLine("qb_s_num : " + b.qb_s_num);
                ////            //    //////Console.WriteLine("qb03 : " + b.wqb01);
                ////            //    //////Console.WriteLine("enrty : " + b.wqb99);
                ////            //}
                ////        };
                ////        tmpans_list.Clear();
                ////        anslist = null;
                ////        //////Console.WriteLine("anslist@@@" + anslist);

                ////        var label_check = new Label // 選項
                ////        {
                ////            Text = j,
                ////            TextColor = Color.Black,
                ////            FontSize = 20
                ////        };

                ////        var stack_check = new StackLayout // checkbox跟選項
                ////        {
                ////            Orientation = StackOrientation.Horizontal,
                ////            Children = { check_box, label_check }
                ////        };

                ////        //var ques_all_check = new StackLayout
                ////        //{
                ////        //    Orientation = StackOrientation.Horizontal,
                ////        //    Children = { stack_check, stack }
                ////        //};

                ////        var stack_enrty = new StackLayout
                ////        {
                ////            Orientation = StackOrientation.Vertical,
                ////            Children = { stack_check, entny }
                ////        };
                ////        stack_ques.Children.Add(stack_enrty);


                ////        //var final_stack = new StackLayout
                ////        //{
                ////        //    Orientation = StackOrientation.Horizontal,
                ////        //    Children = { stack_ques, label_que_name }
                ////        //};
                ////    }


                ////    quesStack.Children.Add(stack); // w

                ////    //quesStack.Children.Add(final_stack);
                ////    //quesStack.Children.Add(label_que_name);
                ////    //quesStack.Children.Add(stack_ques);
                ////    var final_stack = new StackLayout
                ////    {
                ////        Orientation = StackOrientation.Vertical,
                ////        Children = { label_que_name, stack_ques }
                ////    };


                ////    Frame frame = new Frame // frame包上面那個stacklayout
                ////    {
                ////        Padding = new Thickness(10, 5, 10, 5),
                ////        BackgroundColor = Color.FromHex("eddcd2"),
                ////        CornerRadius = 10,
                ////        HasShadow = false,
                ////        Content = final_stack
                ////    };
                ////    quesStack.Children.Add(frame);



                ////}
                
            }
            if (RepeatOrNotList[questionList.ClientName] == -1)
            {
                ////Console.WriteLine("reapetin~~~ ");
                //Console.WriteLine("_QB_S_NUM@@@@222 " + questionList.qb_s_num);
                var same_ans = new Button // 更多題目的回饋單
                {
                    Text = "同上",
                    CornerRadius = 60,
                    BackgroundColor = Color.FromHex("ddbea9"),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    WidthRequest = 100,
                    ////Padding = new Thickness(50, 5, 50, 5),
                    ////Margin = new Thickness(5, 0, 5, 0),
                    FontSize = 18
                };

                //var more_form = new Button // 更多題目的回饋單
                //{
                //    Text = "更多",
                //    CornerRadius = 60,
                //    BackgroundColor = Color.FromHex("ddbea9"),
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    WidthRequest = 100,
                //    //Padding = new Thickness(50, 5, 50, 5),
                //    //Margin = new Thickness(5, 0, 5, 0),
                //    FontSize = 18
                //};

                //more_form.Clicked += (object sender, EventArgs e) =>
                //{
                //    Navigation.PushAsync(new MoreFormView());
                //};
                var button_stack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = { same_ans }
                };
                same_ans.Clicked += (object sender, EventArgs e) =>
                {
                    // 把同名字問卷答案抓出來，填入這題，並存入資料庫
                    //////Console.WriteLine("Buttonname~~~" + questionList.ClientName);
                    ////////Console.WriteLine("namecount~~~" + MapView.AccDatabase.GetItemsName(questionList.ClientName).Count());
                    if (MapView.AccDatabase.GetAccountAsync2().Count() > 0) // database裡面有資料
                    {

                        for (int b = 0; b < MapView.AccDatabase.GetAccountAsync2().Count(); b++)
                        {
                            //int num = MapView.AccDatabase.GetAccountAsync2().Count();
                            var a = MapView.AccDatabase.GetAccountAsync(b);
                            //////Console.WriteLine("count~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                            //////Console.WriteLine("b~~" + b);
                            List<TempAccount> SQLlist = MapView.AccDatabase.GetAccountAsyncToList(b);
                            //////Console.WriteLine("countSQL~~~" + SQLlist.Count());
                            for (int i = 0; i < SQLlist.Count(); i++)
                            {
                                //////Console.WriteLine("i~~~" + i);
                                //////Console.WriteLine("NAME~~~" + SQLlist[i].ClientName);
                                //////Console.WriteLine("sq_s_num~~" + SQLlist[i].qb_s_num);
                                //////Console.WriteLine("orser~~" + SQLlist[i].qb_order);
                                //////Console.WriteLine("wqb01~~~" + SQLlist[i].wqb01);
                                //////Console.WriteLine("wqh~~ " + questionList.wqh_s_num);
                                //////Console.WriteLine("qw~~ " + questionList.qh_s_num);


                            }
                            for (int k = SQLlist.Count() - 1; k > 0; k = k - 1)
                            {
                                //////Console.WriteLine("IN~~~");
                                var total = tmp_name_list.Count(s => s == SQLlist[k].ClientName);
                                Console.WriteLine("total~~~" + total);
                                Console.WriteLine("qbs~~~~" + questionList.qbs.Count());
                                Console.WriteLine("listcount~~ " + tmp_num_list.Count());
                                //if (!tmp_name_list.Contains(SQLlist[k].ClientName))
                                //{
                                if (SQLlist[k].ClientName == questionList.ClientName)
                                {
                                   Console.WriteLine("qqname~~ " + questionList.ClientName);
                                    
                                    string _qbsnum = SQLlist[k].qb_s_num;
                                    if (tmp_num_list.Count() != 0)
                                    {
                                        //////Console.WriteLine("SQList_qb~~~" + SQLlist[k].qb_s_num);
                                        //////Console.WriteLine("tmp_qb~~" + tmp_num_list[tmp_num_list.Count() - 1]);
                                        //////Console.WriteLine("equal~~" + SQLlist[k].qb_s_num.Equals(tmp_num_list[tmp_num_list.Count() - 1]));
                                        Console.WriteLine("total222~~~" + total);
                                        Console.WriteLine("qbs222~~~~" + questionList.qbs.Count());
                                        if(total != questionList.qbs.Count())
                                        {
                                            if (SQLlist[k].qb_s_num.Equals(tmp_num_list[tmp_num_list.Count() - 1]) == false) ;
                                            {
                                                Console.WriteLine("IN~~~~LA~");
                                                Console.WriteLine("Qname~~~" + questionList.ClientName);
                                                Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                                                Console.WriteLine("qb_s_num~~ " + questionList.qb_s_num);
                                                Console.WriteLine("qb_s_num~~2~" + SQLlist[k].qb_s_num);
                                                Console.WriteLine("wqb01~~2~" + SQLlist[k].wqb01);
                                                tmp_name_list.Add(SQLlist[k].ClientName);
                                                tmp_num_list.Add(SQLlist[k].qb_s_num);
                                                _sameans = SQLlist[k].wqb01;
                                                _ans = SQLlist[k].qb_order;
                                                //Console.WriteLine("listcount~~ " + tmp_num_list.Count());
                                                //////Console.WriteLine("k~~~" + k);
                                                //////Console.WriteLine("ClientName~~1~" + SQLlist[k].ClientName);
                                                //////Console.WriteLine("qb_s_num~~1~" + SQLlist[k].qb_s_num);
                                                //////Console.WriteLine("wqb01~~1~" + SQLlist[k].wqb01);
                                                ////////Console.WriteLine("ClientName~~2~" + SQLlist[k-1].ClientName);
                                                ////////Console.WriteLine("qb_s_num~~2~" + SQLlist[k-1].qb_s_num);
                                                ////////Console.WriteLine("wqb01~~2~" + SQLlist[k-1].wqb01);

                                                ////////Console.WriteLine("wqh_s_num~~~" + TempNameList.wqh_s_num);
                                                //////Console.WriteLine("qh_s_num~~~" + SQLlist[k].qb_s_num);
                                                //Console.WriteLine("sameans~~~" + _sameans);
                                                //tmp_name_list.Add(SQLlist[k-1].ClientName);
                                                //tmp_num_list.Add(SQLlist[k-1].qb_s_num);
                                                //_sameans2 = SQLlist[k-1].wqb01;
                                                //string _qbsnum2 = SQLlist[k-1].qb_s_num;

                                                if(SQLlist[k].qb_order == "1")
                                                {
                                                    if (_sameans == "否")
                                                    {
                                                        color = "Red";
                                                    }
                                                    else
                                                    {
                                                        color = "Green";
                                                    }
                                                }
                                                else
                                                {
                                                    if (_sameans == "是" || _sameans == "未發")
                                                    {
                                                        color = "Red";
                                                    }
                                                    else
                                                    {
                                                        color = "Green";
                                                    }
                                                }
                                                
                                                ResetSaveToDB(questionList.wqh_s_num, SQLlist[k].qb_order, color);

                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum, _sameans, questionList.ClientName, _ans);
                                                //QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum2, _sameans2, questionList.ClientName);

                                                ////////Console.WriteLine("count~~~" + count);
                                                if (_sameans == "是")
                                                {
                                                    ANS = 0;
                                                    ANS2 = Convert.ToString(ANS);
                                                }
                                                else
                                                {
                                                    ANS = 1;
                                                    ANS2 = Convert.ToString(ANS);
                                                }
                                                //if(_sameans2 == "是")
                                                //{
                                                //    ANS = 0;
                                                //    ANS3 = Convert.ToString(ANS);
                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS3 = Convert.ToString(ANS);
                                                //}
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = _qbsnum, // 問題編號(第幾題)

                                                    wqb01 = _sameans // 答案

                                                };
                                                //var check2 = new checkInfo
                                                //{
                                                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                //    qb_s_num = _qbsnum2, // 問題編號(第幾題)
                                                //    wqb01 = _sameans2 // 答案

                                                //};
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = _qbsnum, // 問題編號(第幾題)
                                                    qb_order = _ans,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                //var check4 = new checkInfo
                                                //{
                                                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                //    qb_s_num = _qbsnum2, // 問題編號(第幾題)
                                                //    wqb01 = ANS3 // 答案

                                                //};
                                                ////////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                                                ////////Console.WriteLine("qh~~~" + questionList.qh_s_num);
                                                ////////Console.WriteLine("_QB_S_NUM~~~~" + _QB_S_NUM);
                                                //
                                                //////Console.WriteLine("wqb01~~~" + ANS2);

                                                ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList2.RemoveAll(x => x.wqh_s_num == questionList.wqh_s_num && x.qb_order == _ans);
                                                checkList.Add(check); // for check
                                                                      //checkList.Add(check2); // for check
                                                checkList2.Add(check3); // for save
                                                                        //checkList2.Add(check4); // for save
                                                isReset = true;
                                                reset();
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("IN~~~~BB~");
                                        Console.WriteLine("Qname~~~" + questionList.ClientName);
                                        Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                                        Console.WriteLine("qb_s_num~~ " + questionList.qb_s_num);
                                        foreach (var j in questionList.qbs)
                                        {
                                            if (SQLlist[k].qb_s_num == j.qb_s_num)
                                            {
                                                Console.WriteLine("qb_s_num~~0~" + j.qb_s_num);
                                                Console.WriteLine("wqb01~~0~" + SQLlist[k].wqb01);
                                                tmp_name_list.Add(SQLlist[k].ClientName);
                                                tmp_num_list.Add(SQLlist[k].qb_s_num);
                                                _sameans = SQLlist[k].wqb01;

                                                //////Console.WriteLine("k~~~" + k);
                                                //////Console.WriteLine("ClientName~~1~" + SQLlist[k].ClientName);
                                                Console.WriteLine("qb_s_num~~1~" + SQLlist[k].qb_s_num);
                                                Console.WriteLine("wqb01~~1~" + SQLlist[k].wqb01);
                                                ////////Console.WriteLine("ClientName~~2~" + SQLlist[k-1].ClientName);
                                                ////////Console.WriteLine("qb_s_num~~2~" + SQLlist[k-1].qb_s_num);
                                                ////////Console.WriteLine("wqb01~~2~" + SQLlist[k-1].wqb01);

                                                ////////Console.WriteLine("wqh_s_num~~~" + TempNameList.wqh_s_num);
                                                //////Console.WriteLine("qb_s_num~~~" + SQLlist[k].qb_s_num);
                                                ////////Console.WriteLine("sameans~~~" + _sameans);
                                                //tmp_name_list.Add(SQLlist[k-1].ClientName);
                                                //tmp_num_list.Add(SQLlist[k-1].qb_s_num);
                                                //_sameans2 = SQLlist[k-1].wqb01;
                                                //string _qbsnum2 = SQLlist[k-1].qb_s_num;

                                                if (SQLlist[k].qb_order == "1")
                                                {
                                                    if (_sameans == "否")
                                                    {
                                                        color = "Red";
                                                    }
                                                    else
                                                    {
                                                        color = "Green";
                                                    }
                                                }
                                                else
                                                {
                                                    if (_sameans == "是" || _sameans == "未發")
                                                    {
                                                        color = "Red";
                                                    }
                                                    else
                                                    {
                                                        color = "Green";
                                                    }
                                                }

                                                ResetSaveToDB(questionList.wqh_s_num, SQLlist[k].qb_order, color);

                                                QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum, _sameans, questionList.ClientName, _ans);
                                                //QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum2, _sameans2, questionList.ClientName);

                                                ////////Console.WriteLine("count~~~" + count);
                                                if (_sameans == "是")
                                                {
                                                    ANS = 0;
                                                    ANS2 = Convert.ToString(ANS);
                                                }
                                                else
                                                {
                                                    ANS = 1;
                                                    ANS2 = Convert.ToString(ANS);
                                                }
                                                //if(_sameans2 == "是")
                                                //{
                                                //    ANS = 0;
                                                //    ANS3 = Convert.ToString(ANS);
                                                //}
                                                //else
                                                //{
                                                //    ANS = 1;
                                                //    ANS3 = Convert.ToString(ANS);
                                                //}
                                                var check = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = _qbsnum, // 問題編號(第幾題)
                                                    wqb01 = _sameans // 答案

                                                };
                                                //var check2 = new checkInfo
                                                //{
                                                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                //    qb_s_num = _qbsnum2, // 問題編號(第幾題)
                                                //    wqb01 = _sameans2 // 答案

                                                //};
                                                var check3 = new checkInfo
                                                {
                                                    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                    qb_s_num = _qbsnum, // 問題編號(第幾題)
                                                    qb_order = _ans,
                                                    wqb01 = ANS2 // 答案

                                                };
                                                //var check4 = new checkInfo
                                                //{
                                                //    wqh_s_num = questionList.wqh_s_num, // 問卷編號
                                                //    qh_s_num = questionList.qh_s_num, // 工作問卷編號
                                                //    qb_s_num = _qbsnum2, // 問題編號(第幾題)
                                                //    wqb01 = ANS3 // 答案

                                                //};
                                                ////////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                                                ////////Console.WriteLine("qh~~~" + questionList.qh_s_num);
                                                ////////Console.WriteLine("_QB_S_NUM~~~~" + _QB_S_NUM);
                                                //
                                                //////Console.WriteLine("wqb01~~~" + ANS2);

                                                ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                                                checkList.Add(check); // for check
                                                                      //checkList.Add(check2); // for check
                                                checkList2.Add(check3); // for save
                                                                        //checkList2.Add(check4); // for save
                                                isReset = true;
                                                reset();
                                            }
                                        }
                                        
                                    }


                                    //}
                                }

                            }
                            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
                            //foreach (var TempNameList in a)
                            //{
                            //    var total = tmp_name_list.Count(s => s == TempNameList.ClientName);
                            //    //////Console.WriteLine("total~~~" + total);
                            //    //////Console.WriteLine("qbs~~~~" + questionList.qbs.Count());
                            //    if (!tmp_name_list.Contains(TempNameList.ClientName) || total != 2)
                            //    {
                            //        if (TempNameList.ClientName == questionList.ClientName)
                            //        {

                            //            //////Console.WriteLine("IN~~~~LA~");
                            //            //////Console.WriteLine("Qname~~~" + questionList.ClientName);
                            //            //////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                            //            tmp_name_list.Add(TempNameList.ClientName);
                            //            tmp_num_list.Add(TempNameList.qb_s_num);
                            //            _sameans = TempNameList.wqb01;
                            //            string _qbsnum = TempNameList.qb_s_num;
                            //            //////Console.WriteLine("qbsum~~~" + _qbsnum);
                            //            //////Console.WriteLine("ClientName~~~" + TempNameList.ClientName);

                            //            ////////Console.WriteLine("wqh_s_num~~~" + TempNameList.wqh_s_num);
                            //            //////Console.WriteLine("qb_s_num~~~" + TempNameList.qb_s_num);
                            //            ////////Console.WriteLine("sameans~~~" + _sameans);


                            //            QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum, _sameans, questionList.ClientName);

                            //            ////////Console.WriteLine("count~~~" + count);
                            //            if (_sameans == "是")
                            //            {
                            //                ANS = 0;
                            //                ANS2 = Convert.ToString(ANS);
                            //            }
                            //            else
                            //            {
                            //                ANS = 1;
                            //                ANS2 = Convert.ToString(ANS);
                            //            }
                            //            var check = new checkInfo
                            //            {
                            //                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                qb_s_num = _qbsnum, // 問題編號(第幾題)
                            //                wqb01 = _sameans // 答案

                            //            };
                            //            var check3 = new checkInfo
                            //            {
                            //                wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //                qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //                qb_s_num = _qbsnum, // 問題編號(第幾題)
                            //                qb_order = i.qb_order,
                            //                wqb01 = ANS2 // 答案

                            //            };
                            //            //////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                            //            //////Console.WriteLine("qh~~~" + questionList.qh_s_num);
                            //            //////Console.WriteLine("_QB_S_NUM~~~~" + _QB_S_NUM);
                            //            //////Console.WriteLine("wqb01~~~" + ANS2);

                            //            ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //            checkList.Add(check); // for check
                            //            checkList2.Add(check3); // for save
                            //            reset();

                            //        }
                            //    }

                            //}
                            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                            //var TempNameList = MapView.AccDatabase.GetAccountAsync(b).Last();
                            //var total = tmp_name_list.Count(s => s == TempNameList.ClientName);
                            ////////Console.WriteLine("total~~~" + total);
                            ////////Console.WriteLine("qbs~~~~" + questionList.qbs.Count());
                            //if (!tmp_name_list.Contains(TempNameList.ClientName) || total != 2)
                            //{
                            //    if (TempNameList.ClientName == questionList.ClientName)
                            //    {

                            //        //////Console.WriteLine("IN~~~~LA~");
                            //        //////Console.WriteLine("Qname~~~" + questionList.ClientName);
                            //        //////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                            //        tmp_name_list.Add(TempNameList.ClientName);
                            //        tmp_num_list.Add(TempNameList.qb_s_num);
                            //        _sameans = TempNameList.wqb01;
                            //        string _qbsnum = TempNameList.qb_s_num;
                            //        //////Console.WriteLine("qbsum~~~" + _qbsnum);
                            //        //////Console.WriteLine("ClientName~~~" + TempNameList.ClientName);

                            //        ////////Console.WriteLine("wqh_s_num~~~" + TempNameList.wqh_s_num);
                            //        //////Console.WriteLine("qb_s_num~~~" + TempNameList.qb_s_num);
                            //        ////////Console.WriteLine("sameans~~~" + _sameans);


                            //        QuesSaveToSQLite(questionList.wqh_s_num, questionList.qh_s_num, _qbsnum, _sameans, questionList.ClientName);

                            //        ////////Console.WriteLine("count~~~" + count);
                            //        if (_sameans == "是")
                            //        {
                            //            ANS = 0;
                            //            ANS2 = Convert.ToString(ANS);
                            //        }
                            //        else
                            //        {
                            //            ANS = 1;
                            //            ANS2 = Convert.ToString(ANS);
                            //        }
                            //        var check = new checkInfo
                            //        {
                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            qb_s_num = _qbsnum, // 問題編號(第幾題)
                            //            wqb01 = _sameans // 答案

                            //        };
                            //        var check3 = new checkInfo
                            //        {
                            //            wqh_s_num = questionList.wqh_s_num, // 問卷編號
                            //            qh_s_num = questionList.qh_s_num, // 工作問卷編號
                            //            qb_s_num = _qbsnum, // 問題編號(第幾題)
                            //            qb_order = i.qb_order,
                            //            wqb01 = ANS2 // 答案

                            //        };
                            //        //////Console.WriteLine("wqh~~~" + questionList.wqh_s_num);
                            //        //////Console.WriteLine("qh~~~" + questionList.qh_s_num);
                            //        //////Console.WriteLine("_QB_S_NUM~~~~" + _QB_S_NUM);
                            //        //////Console.WriteLine("wqb01~~~" + ANS2);

                            //        ////////Console.WriteLine("i.qb_s_num####~~" + i.qb_s_num);
                            //        checkList.Add(check); // for check
                            //        checkList2.Add(check3); // for save

                            //        reset();

                            //    }
                            //}
                        }
                    }
                    ////////Console.WriteLine("questionList.wqh_s_num~~" + questionList.wqh_s_num);
                    ////////Console.WriteLine("questionList.qh_s_num~~" + questionList.qh_s_num);
                    ////////Console.WriteLine("i.qb_s_num~~" + _QB_S_NUM);
                    ////////Console.WriteLine("wqb01~~" + _sameans);
                    ////////Console.WriteLine("name~~~" + questionList.ClientName);
                };
                quesStack.Children.Add(button_stack);
                return null;
            }
            else
            {
                //var more_form = new Button // 更多題目的回饋單
                //{
                //    Text = "更多",
                //    CornerRadius = 60,
                //    BackgroundColor = Color.FromHex("ddbea9"),
                //    Padding = new Thickness(50, 5, 50, 5),
                //    Margin = new Thickness(5, 0, 5, 0),
                //    FontSize = 18
                //};

                //more_form.Clicked += (object sender, EventArgs e) =>
                //{
                //    Navigation.PushAsync(new MoreFormView());
                //};
                //quesStack.Children.Add(more_form);
                return null;
            }
            //return null;
        }
        

        public void ChooseSaveToDB(string _clnName, bool _isChoose)
        {
            ChooseDB.SaveAccountAsync(new Choose
            {
                ClientName = _clnName,
                ischoose = _isChoose,
               
            });
        }

        public void ResetSaveToDB(string _wqh_s_num, string _qb_order, string _color)
        {
            ResetDB.SaveAccountAsync(new Reset
            {
                wqh_s_num = _wqh_s_num,
                qb_order = _qb_order,
                color = _color

            });
        }

        public void EntrySaveToDB(string _clnName, string _wqh_s_num, string _qb_s_num, string _qb_order)
        {
            EntryDB.SaveAccountAsync(new Entry_DB
            {
                ClientName = _clnName,
                wqh_s_num = _wqh_s_num,
                qb_s_num = _qb_s_num,
                qb_order = _qb_order
            });
        }

        public void QuesSaveToSQLite(string _wqh_s_num, string _qh_s_num, string _qb_s_num, string _wqb01, string _clnName, string _qb_order)
        {
            //////Console.WriteLine("QUESADD~~~~");
            MapView.AccDatabase.SaveAccountAsync(new TempAccount
            {
                ClientName = _clnName,
                wqh_s_num = _wqh_s_num, // 問卷編號
                qh_s_num = _qh_s_num, // 工作問卷編號
                qb_order = _qb_order,
                qb_s_num = _qb_s_num, // 問題編號
                wqb01 = _wqb01,
                
                //wqb99 = entrytxt
            });
        }
        public void EntrySaveToSQLite(string _wqh_s_num, string _qh_s_num, string _qb_s_num, string _clnName, string _qb_order,string _wqb01, string _entry)
        {
            //////Console.WriteLine("QUESADD~~~~");
            MapView.AccDatabase.SaveAccountAsync(new TempAccount
            {
                ClientName = _clnName,
                wqh_s_num = _wqh_s_num, // 問卷編號
                qh_s_num = _qh_s_num, // 工作問卷編號
                qb_order = _qb_order,
                qb_s_num = _qb_s_num, // 問題編號
                wqb01 = _wqb01,
                wqb99 = _entry

                //wqb99 = entrytxt
            });
        }

        public void EntrytxtSaveDB(string _clnName, string _wqh_s_num, string _entrytxt)
        {
            EntrytxtDB.SaveAccountAsync(new Entry_txt
            {
                ClientName = _clnName,
                wqh_s_num = _wqh_s_num,
                entrytxt = _entrytxt
            });
        }

        public void AddSaveToDB(string _wqh_s_num, string _qb_order)
        {
            TempAddDB.SaveAccountAsync(new TmpAdd
            {
                wqh_s_num = _wqh_s_num,
                qb_order = _qb_order
            }) ;
        }

        public void Add_elseSaveToDB(string _wqh_s_num, string _qb_order)
        {
            TempAddDB_else.SaveAccountAsync(new TmpAdd
            {
                wqh_s_num = _wqh_s_num,
                qb_order = _qb_order
            });
        }

         


        private async void post_questionClicked(object sender, EventArgs e)
        {
            //////Console.WriteLine("TYPE~~~" + Qtype);
            //////Console.WriteLine("post~~~ ");


            //foreach (var anslist in checkList2)
            //{
            //    ////Console.WriteLine("ans_wqh_s_num : " + anslist.wqh_s_num);
            //    ////Console.WriteLine("ans_qb_s_num : " + anslist.qb_s_num);
            //    ////Console.WriteLine("ans_qb03 : " + anslist.wqb01);
            //    ////Console.WriteLine("ans_enrty : " + anslist.wqb99);
            //    ////Console.WriteLine("Q_count~~ " + questionnaireslist.Count());
            //    for (int i = 0; i < questionnaireslist.Count(); i++)
            //    {
            //        if (anslist.wqh_s_num == questionnaireslist[i].wqh_s_num)
            //        {
            //            for (int j = 0; j < questionnaireslist[i].qbs.Count(); j++)
            //            {
            //                ////Console.WriteLine("count~~~ " + questionnaireslist[i].qbs.Count());
            //                if (anslist.qb_order == "5")
            //                {
            //                    checkListtmp.Add(anslist);
            //                }
            //            }
            //            // 等這個wqh_s_num的這個qb_order都處理完
            //            // 抓出這筆訂單這個問題的最後一筆答案，並加到上傳list中
            //            checkInfo checkListfinal = checkListtmp[checkListtmp.Count() - 1];
            //            checkListupload.Add(checkListfinal);
            //            checkListtmp.Clear();
            //        }
            //    }
            //}
            //Content = ViewService.Loading();
            foreach (var b in checkList2)
            {
                //Console.WriteLine("LALALApost~~");
                //Console.WriteLine("wqh_s_num : " + b.wqh_s_num);
                //Console.WriteLine("qb_s_num : " + b.qb_s_num);
                //Console.WriteLine("qb03 : " + b.wqb01);
                //Console.WriteLine("enrty : " + b.wqb99);
            }
            work_data resault = new work_data()
            {
                upload_check = checkList2,
                //upload_check = checkListupload,
            };
            bool web_res1 = await web.Save_Questionaire(MainPage.token, resault);
            //////Console.WriteLine("WEBBBB" + web_res1);
            try
            {
                
                if (web_res1 == true)
                {
                    //yesnoList.Clear();
                    checkList2.Clear();
                    checkList.Clear();
                    tmp_name_list.Clear();
                    tmp_num_list.Clear();
                    name_list.Clear();
                    MapView.AccDatabase.DeleteAll();
                    ChooseDB.DeleteAll();
                    if (ChooseDB.GetAccountAsync2().Count() == 0)
                    {
                        //////Console.WriteLine("chooseSUCESS");
                    }
                    else
                    {
                        //////Console.WriteLine("chooseFAIL");
                        //fooDoggyDatabase.DeleteAll();
                        //////Console.WriteLine("failQAQ~~~" + ChooseDB.GetAccountAsync2().Count());
                    }
                    ResetDB.DeleteAll();
                    if (ResetDB.GetAccountAsync2().Count() == 0)
                    {
                        //////Console.WriteLine("resetSUCESS");
                    }
                    else
                    {
                        //////Console.WriteLine("resetFAIL");
                        //fooDoggyDatabase.DeleteAll();
                        //////Console.WriteLine("failQAQ~~~" + ResetDB.GetAccountAsync2().Count());
                    }
                    EntryDB.DeleteAll();
                    if (EntryDB.GetAccountAsync2().Count() == 0)
                    {
                        //////Console.WriteLine("entrySUCESS");
                    }
                    else
                    {
                        //////Console.WriteLine("entryFAIL");
                        //fooDoggyDatabase.DeleteAll();
                        //////Console.WriteLine("failQAQ~~~" + EntryDB.GetAccountAsync2().Count());
                    }
                    EntrytxtDB.DeleteAll();
                    if (EntrytxtDB.GetAccountAsync2().Count() == 0)
                    {
                        //////Console.WriteLine("entrytxtSUCESS");
                    }
                    else
                    {
                        //////Console.WriteLine("entrytxtFAIL");
                        //fooDoggyDatabase.DeleteAll();
                        //////Console.WriteLine("failQAQ~~~" + EntrytxtDB.GetAccountAsync2().Count());
                    }
                    TempAddDB.DeleteAll();
                    TempAddDB_else.DeleteAll();
                    reset();
                    //////Console.WriteLine("testviewaccdatabasedeletesuccess~~");
                    //await Navigation.PopAsync();
                    await DisplayAlert("上傳結果", "上傳成功！", "ok");
                   
                    
                    ////////Console.WriteLine("Single~~~~");
                }
            } 
            catch (Exception ex)
            {
                await DisplayAlert("提示", "上傳失敗！", "ok");
                //////Console.WriteLine("Error_upload~~~ " + ex.ToString());
            }

            

        }

        

        private void Messager()
        {
            try
            {
               
                MessagingCenter.Subscribe<HomeView, bool>(this, "SET_FORM", (sender, arg) =>
                {
                    // do something when the msg "UPDATE_BONUS" is recieved
                    if (arg)
                    {
                        //if(MainPage.Loginway == "Enter")
                        //{
                        //    //questionnaireslist = new List<questionnaire>();
                        //    questionnaireslist.Clear();
                        //    //checkList = new List<checkInfo>();
                        //    checkList.Clear();
                        //    entny = new Entry();
                        //    //entny = null;
                        //    quesStack.Children.Clear();
                        //    //TmpCheckList = new Dictionary<string, bool>();
                        //    TmpCheckList.Clear();
                        //    //////Console.WriteLine("MESS");
                        //    setView();
                        //}
                        //else
                        //{
                        //    setView();
                        //}
                        
                        //checkList = new List<checkInfo>();
                       
                        entny = new Entry();
                        //entny = null;
                        
                        //TmpCheckList = new Dictionary<string, bool>();
                        //TmpCheckList.Clear();
                        //////Console.WriteLine("MESS");
                        setView();
                    }
                });
                MessagingCenter.Subscribe<HomeView2, bool>(this, "SET_FORM", (sender, arg) =>
                {
                    // do something when the msg "UPDATE_BONUS" is recieved
                    if (arg)
                    {
                        //if(MainPage.Loginway == "Enter")
                        //{
                        //    //questionnaireslist = new List<questionnaire>();
                        //    questionnaireslist.Clear();
                        //    //checkList = new List<checkInfo>();
                        //    checkList.Clear();
                        //    entny = new Entry();
                        //    //entny = null;
                        //    quesStack.Children.Clear();
                        //    //TmpCheckList = new Dictionary<string, bool>();
                        //    TmpCheckList.Clear();
                        //    //////Console.WriteLine("MESS");
                        //    setView();
                        //}
                        //else
                        //{
                        //    setView();
                        //}

                        //checkList = new List<checkInfo>();

                        entny = new Entry();
                        //entny = null;

                        //TmpCheckList = new Dictionary<string, bool>();
                        //TmpCheckList.Clear();
                        //////Console.WriteLine("MESS");
                        setView();
                    }
                });
                MessagingCenter.Subscribe<HomeViewHelperAndDiliver, bool>(this, "SET_FORM", (sender, arg) =>
                {
                    // do something when the msg "UPDATE_BONUS" is recieved
                    if (arg)
                    {
                        //if(MainPage.Loginway == "Enter")
                        //{
                        //    //questionnaireslist = new List<questionnaire>();
                        //    questionnaireslist.Clear();
                        //    //checkList = new List<checkInfo>();
                        //    checkList.Clear();
                        //    entny = new Entry();
                        //    //entny = null;
                        //    quesStack.Children.Clear();
                        //    //TmpCheckList = new Dictionary<string, bool>();
                        //    TmpCheckList.Clear();
                        //    //////Console.WriteLine("MESS");
                        //    setView();
                        //}
                        //else
                        //{
                        //    setView();
                        //}

                        //checkList = new List<checkInfo>();

                        entny = new Entry();
                        //entny = null;

                        //TmpCheckList = new Dictionary<string, bool>();
                        //TmpCheckList.Clear();
                        //////Console.WriteLine("MESS");
                        setView();
                    }
                });
                MessagingCenter.Subscribe<MapView, bool>(this, "SET_TMP_FORM", (sender, arg) =>
                {
                    // do something when the msg "UPDATE_BONUS" is recieved
                    if (arg)
                    {
                        reset();
                        //////Console.WriteLine("RESET~~");
                        //////Console.WriteLine("COUNT111~~~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                       // //////Console.WriteLine("UUUU~~" + MapView.AccDatabase.GetAccountAsync2().Count());
                    }
                });
                MessagingCenter.Subscribe<MemberView, bool>(this, "OUT", (sender, arg) =>
                {
                    if (arg)
                    {
                        Navigation.PushAsync(new TestView());
                    }
                });
                MessagingCenter.Subscribe<MemberView, bool>(this, "LOGOUT", (sender, arg) =>
                {
                    if (arg)
                    {
                        //Navigation.PushAsync(new TestView());
                        //////Console.WriteLine("Logout~~~~testview.tmpchecklist.clear_success");
                        TmpCheckList.Clear();
                        questionnaireslist.Clear();
                        checkList.Clear();
                        quesStack.Children.Clear();
                    }
                });
            }
            catch (Exception ex)
            {
                //////Console.WriteLine(ex.ToString());
            }
        }

        protected override void OnAppearing()
        {
            
        }


    }
}