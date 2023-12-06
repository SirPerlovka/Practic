using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PP
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        DispatcherTimer _timer; //Обозначение диспетчера
        TimeSpan _time; //Время таймера
        public Administrator(string post)
        {
            InitializeComponent();
            //Разграничение прав пользователей
            if (post != null) //Если должность есть
            {
                if (post == "Администратор") 
                {
                    tbUser.Text = "Вход выполнен на форму Администратора";
                }
                else if (post == "Старший смены")
                {
                    tbUser.Text = "Вход выполнен на форму Старшего смены";
                }
                else if (post == "Продавец")
                {
                    tbUser.Text = "Вход выполнен на форму Продавца";
                }
                else if (post == "NULL")
                {
                    tbUser.Text = "Вход выполнен на форму Клиента";
                }
            }
            _timer.Start(); //Запускаем таймер

            _time = TimeSpan.FromSeconds(15); //Установка времени таймера

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c"); //Показываем таймер
                if (_time == TimeSpan.Zero) _timer.Stop(); //Если таймер равен нулю то выключаем
                _time = _time.Add(TimeSpan.FromSeconds(-1)); //Присваем ко времени таймера -1
                _timer.Tick += new EventHandler(Timer_Tick); //Вызываем метод срабатывания таймера по тикам
            }, Application.Current.Dispatcher);
        }
        int i = 0; //Обозначение тиков таймера
        private void Timer_Tick(object sender, EventArgs e) //Действие по истечении таймера
        {
            i++;
            if (i == 120) //Если тики таймера то закрываем форму
            {
                MainWindow mw = new MainWindow(); //Возвращаемся на первую форму
                mw.Show(); //Открываем первую форму
                Close(); //Закрываем текущую форму
            }
        }
    }
}
