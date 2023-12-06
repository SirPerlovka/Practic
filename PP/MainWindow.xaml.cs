using PP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer; //Обозначение диспетчера
        TimeSpan _time; //Время таймера
        public MainWindow()
        {
            InitializeComponent();
        }

        Entity.PPEntities entities = new Entity.PPEntities(); //Взаимодействие с базой данных
        int checkLog = 0; //Проверка на правильность ввода логина
        int error = 0; //Проверка количества ошибок
        private List<SolidColorBrush> brushes; //Список используемых цветов для капчи
        private Random rndColor; //Рандомные цвета на капчу

        private static string RndStr1(int len) //Метод рандомных символов для капчи
        {
            string s = "", symb = "QWERTYUIPADFGHJKLZXCVBNM123456789"; //Символы для использования в капче
            Random rnd = new Random(); 
            for (int i = 0; i < len; i++)
                s += symb[rnd.Next(0, symb.Length)] + " "; //Выбор символов для использования в капче + пробел
            return s; //Возврат выбраных символов
        }
        
        private void AutorizationOpen(object sender, RoutedEventArgs e) //Кнопка авторизации
        {
            string text = login.Text; //Проверяем логин
            string check = "@namecomp"; //Ищем в логине @namecomp
            bool checkOpen = text.Contains(check); //Метод поиска в логине выбранного слова

            rndColor = new Random();
            brushes = new List<SolidColorBrush> //заполняем цвета, которые будет принимать капча
  {//Тут цвета которые используются в капче
    Brushes.AliceBlue,
    Brushes.AntiqueWhite,
    Brushes.Aqua,
    Brushes.Aquamarine,
    Brushes.Brown,
    Brushes.Green, 
    Brushes.Magenta
  };

            if (checkOpen == true) //Если в логине есть @namecomp значит это сотрудник
            {
                Workers.ItemsSource = App.DataBase.Employes.ToList(); //Взаимодействие с таблицей сотрудников
                //Авторизация пользователя
                entities = App.DataBase; //Взаимодействие с БД
                var user = entities.Employes.Where(c => c.Login == login.Text).OrderBy(c => c.Password); //Сравниваем логин из БД и логин введенный пользователем, группируем по паролю
                //Проверка логина
                foreach (Entity.Employes c in user) //Если логин есть
                {    //Проверка пароля
                    checkLog = 0;
                    if (c.Password == passwordA.Password.ToString()) //Сравниваем пароль из БД и введенный пользователем
                    {
                        string post = c.Post.ToString(); //Обозначение должности
                        string lastname = c.Lastname.ToString(); //Обозначение фамилии
                        string firstname = c.Firstname.ToString(); //Обозначение имени
                        string patronymic = c.Patronymic.ToString(); //Обозначение отчества
                        checkLog = 1; //Если сюда попали значит логин верный и вводить сообщение об ошибке не надо
                        Administrator form = new Administrator(post); //Открываем форму пользователей
                        form.Show(); //Показываем форму пользователей
                        Close(); //Закрываем форму в которой находимся
                        MessageBox.Show($"Вход выполнен под пользователем: {lastname} {firstname} {patronymic}\nДолжность: {post}"); //Выводим сообщение о пользователе который зашел в систему
                    }
                    else //Если пароль не прошел 
                    {
                        checkLog = 1; //Если мы проверяли пароль то логин верный
                        error = error +1; //+1 к ошибке при вводе данных, скоро будет капча
                        MessageBox.Show("Неверный пароль"); //Сообщение об ошибке при вводе пароля
                    }
                }
                if (checkLog == 0) // если пароль не проверяли то логин не верный
                {
                    MessageBox.Show("Неверный логин"); //Сообщение об ошибке что логин не верный
                    error = error + 1; //+1 к ошибке при вводе данных, капча еще ближе
                }
                if (error > 1) //Если ошибок больше 1 то выводим капчу
                {
                    string symbol1 = RndStr1(4); //Обозначаем выбраные рандомные символы
                    im1.Visibility = Visibility.Visible; //Картинка чтобы закрыть пользователю функционал пока он не ввел капчу
                    btCap.Visibility = Visibility.Visible; //Показываем кнопку для работы с капчей
                    tbCap.Visibility = Visibility.Visible; //Показываем текст бокс для ввода капчи
                    cap.Visibility = Visibility.Visible; //Показываем капчу пользователю
                    cap.Background = brushes[rndColor.Next(0, brushes.Count)]; //Меняем цвет фона капчи на рандомный из списка
                    cap.Foreground = brushes[rndColor.Next(0, brushes.Count)]; //Меняем цвет текста капчи на рандомный из списка
                    cap.Text = symbol1; //Присваиваем капче текст
                }
            }
            else //Если должности у пользователя нет, значит он клиент
            {
                Clients.ItemsSource = App.DataBase.Clients.ToList(); //Смотрим таблицу клиентов
                //Авторизация пользователя
                entities = App.DataBase; //Соединяемся с базой
                var user = entities.Clients.Where(c => c.Email == login.Text).OrderBy(c => c.passwort); //Проверяем введенный логин и логин из базы, группируем по паролю
                //Проверка логина
                foreach (Entity.Clients c in user) //Если логин правильный
                {    //Проверка пароля
                    checkLog = 0;
                    if (c.passwort == passwordA.Password.ToString()) //Если пароль введенный пользователем и из базы данных равны
                    {
                        string post = "NULL"; //Отметка что должность равна нулю
                        string lastname = c.Lastname.ToString(); //Обозначение фамилии
                        string firstname = c.Firstname.ToString(); //Обозначение имени
                        string patronymic = c.Patronymic.ToString(); //Обозначение отчества
                        checkLog = 1; //Проверка логина не нужна
                        Administrator form = new Administrator(post); //Переход на форму пользователя
                        form.Show(); //Открываем форму пользователя
                        Close(); //Закрываем текущую форму
                        MessageBox.Show($"Вход выполнен под пользователем: {lastname} {firstname} {patronymic}"); //Пишем сообщение о пользователе который зашел в систему
                    }
                    else //Если пароль не прошел 
                    {
                        checkLog = 1; //Выводить сообщение о логине не надо
                        error = error + 1; //+ к проверке на капчу
                        MessageBox.Show("Неверный пароль"); //Сообщение о неверном вводе пароля
                    }
                }
                if (checkLog == 0) //Если пароль не проверяли значит логин не верный
                {
                    error = error + 1; //+ к проверке на капчу
                    MessageBox.Show("Неверный логин"); //Сообщение о неверном вводе логина
                }
                if (error > 1) //Если ошибок при вводе данных больше одной
                {
                    string symbol1 = RndStr1(4); //Обозначаем символы для капчи
                    im1.Visibility = Visibility.Visible; //Выводим картинку для закрытия функционала от пользователя
                    btCap.Visibility = Visibility.Visible; //Выводим кнопку для работы с капчей
                    tbCap.Visibility = Visibility.Visible; //Выводим текст бокс для работы с капчей
                    cap.Visibility = Visibility.Visible; //Выводим капчу
                    cap.Background = brushes[rndColor.Next(0, brushes.Count)]; //Объявляем цвет фона капчи
                    cap.Foreground = brushes[rndColor.Next(0, brushes.Count)]; //Объявляем цвет текста капчи
                    cap.Text = symbol1; //Вставляем символы в капчу
                }
            }
        }
        int block = 0; //Отсчет для вывода блокировки экрана
        private void CheckCapcha(object sender, RoutedEventArgs e) //Метод проверки капчи
        {
            capCheck.Text = cap.Text.Replace(" ", ""); //Убираем ранее вставленные пробелы из текста для проверки и перекидываем
                                                                  //текст на секретное поле для понимания символов который надо посмотреть а не новые
            if (tbCap.Text == capCheck.Text) //Если пользователь правильно ввел капчу
            {
                error = 0; //Обновляем ошибки ввода данных
                tbCap.Visibility = Visibility.Hidden; //Закрываем поле проверки капчи
                btCap.Visibility = Visibility.Hidden; //Закрываем кнопку работы с капчей
                im1.Visibility = Visibility.Hidden; //Убираем занавес
                cap.Visibility = Visibility.Hidden; //Убираем капчу
            }
            else //Если пользователь неправильно ввел капчу
            {
                string symbol1 = RndStr1(4); //Обновляем символы капчи
                cap.Background = brushes[rndColor.Next(0, brushes.Count)]; //Присваиваем новый цвет фона
                cap.Foreground = brushes[rndColor.Next(0, brushes.Count)]; //Присваем новый цвет текста
                cap.Text = symbol1; //Вставляем обновленные символы в капчу
                block++; //Прибавляем к значению блокировки
                if (block > 1) //Если значение блокировки больше 1
                { //Таймер ширмы
                    _time = TimeSpan.FromSeconds(5); //Установка времени таймера
                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate //Диспетчер
                    {
                        tbTime.Text = _time.ToString("c"); //Выводим текст таймера
                        if (_time == TimeSpan.Zero) _timer.Stop(); //Если таймер равен нулю - выключаем
                        _time = _time.Add(TimeSpan.FromSeconds(-1)); //Добавить ко времени таймера -1
                        _timer.Tick += new EventHandler(Timer_Tick); //Присваеваем каждому тику срабатываение метода
                    }, Application.Current.Dispatcher); //Обновляем таймер при помощи диспетчера
                    _timer.Start(); //Запускаем таймер
                    tbTime.Visibility = Visibility.Visible; //Показываем таймер
                    blockWin.Visibility = Visibility.Visible; //Показываем окно блокировки
                }
            }

        }
        int i = 0; //Обозначение тиков таймера
        private void Timer_Tick(object sender, EventArgs e) //Действие по истечении таймера
        {
            i++;
            if (i == 15) //Если тики таймера то закрываем форму
            {
                i = 0; //Обновляем тики таймера
                blockWin.Visibility = Visibility.Hidden; //Закрываем окно блокировки
                tbTime.Visibility = Visibility.Hidden; //Закрываем текст блок таймера
            }
        }
    }
}
