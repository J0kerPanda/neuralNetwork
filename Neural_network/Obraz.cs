using System;

using System.Drawing; //Для использования класса Bitmap и т.д.
using System.Windows.Forms; //Для использования класса MessageBox и т.д.
using System.IO; //Для использования класса Path и чтения/записи в файл

namespace ObrazNS
{
    /// <summary>
    /// Класс, содержащий и задающий образ изображения
    /// </summary>
    class Obraz
    {
        /// <summary>
        /// Образ изображения в виде булевой матрицы
        /// </summary>
        public bool[,] obraz_matrix { get; private set; }

        /// <summary>
        /// Размер образа по вертикальной оси
        /// </summary>
        public uint Y_border { get; private set; }

        /// <summary>
        /// Размер образа по горизонтальной оси
        /// </summary>
        public uint X_border { get; private set; }

        /// <summary>
        /// Имя файла, из которого получают образ
        /// </summary>
        public String Obraz_File_Name { get; private set; }

        /// <summary>
        /// Получение образа изображения
        /// </summary>
        /// <param name="ImageFilePath">Путь к изображению</param>
        public Obraz( String ImageFilePath )
        {
            SetObrazBorders( ImageFilePath );

            //Первоначальное задание образа (фигура не масштабирована)
            obraz_matrix = new bool[ Y_border, X_border ];

            bool[,] pixel_matrix = GetImgMatrix( ImageFilePath ); //Булева матрица, соответствующая действительными пикселями изображения

            //Масштабирование образа
            ObrazScaling( pixel_matrix );

            //Имя образа
            Obraz_File_Name = Path.GetFileNameWithoutExtension( ImageFilePath ) + DefaultObrazFileNameAddition;

            //WriteObrazLog(Obraz_File_Name);
        }

        /// <summary>
        /// Конструктор пустого образа
        /// </summary>
        public Obraz()
        {
            X_border = 0;
            Y_border = 0;

            obraz_matrix = null;
            Obraz_File_Name = null;
        }

        /// <summary>
        /// Получение размеров образа изображения
        /// </summary>
        /// <param name="ImageFilePath">Путь к файлу изображения</param>
        private void SetObrazBorders( String ImageFilePath )
        {
            try
            {
                Bitmap BitImg = new Bitmap( ImageFilePath );

                X_border = ( uint ) BitImg.Width;
                Y_border = ( uint ) BitImg.Height;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при получении границ образа.", "Образ " + Obraz_File_Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Получение булевой матрицы, соответствующей пикселям изображения
        /// </summary>
        /// <param name="ImageFilePath">Путь к изображению</param>
        /// <returns></returns>
        private bool[,] GetImgMatrix( String ImageFilePath )
        {
            try
            {
                bool[,] pixel_matrix = new bool[ Y_border, X_border ]; //Матрица, заполненная действительными пикселями изображения

                Bitmap BitImg = new Bitmap( ImageFilePath ); //Bitmap-карта изображения

                Color PixelColor = Color.White; //Цвет одного (данного) пикселя изображения
                Color FontColor = System.Drawing.ColorTranslator.FromHtml( "#FFFFFF" ); //Цвет фона

                //Cчитывание пикселей в матрицу
                //Белый цвет - 0, иначе - 1
                for ( int y = 0; y < Y_border; ++y )
                {
                    for ( int x = 0; x < X_border; ++x )
                    {
                        PixelColor = BitImg.GetPixel( x, y ); //Bitmap изображение считывается, перевёрнутое 90 градусов вправо

                        if ( PixelColor == FontColor ) //Cравнение с цветом фона
                            pixel_matrix[ y, x ] = false;
                        else
                            pixel_matrix[ y, x ] = true;

                    }
                }

                return pixel_matrix;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при считывании матрицы изображения.", "Образ " + Obraz_File_Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                return null;
            }
        }

        /// <summary>
        /// Масштабирование образа
        /// </summary>
        /// <param name="pixels">Матрица, заполненная действительными пикселями изображения</param>
        private void ObrazScaling( bool[,] pixels )
        {
            try
            {
                //Границы цифры (координаты границ по соответствующим осям)
                //Начало координат в левом верхнем углу изображения
                uint TopFigureBorder = 0; //Верхний край цифры
                uint BotFigureBorder = 0; //Нижний край цифры
                uint LeftFigureBorder = 0; //Левый край цифры
                uint RightFigureBorder = 0; //Правый край цифры

                //Переменные для прохода по матрице пикселей
                uint x = 0;
                uint y = 0;

                //Коэффициенты масштабирования
                float x_ScaleRatio = 0;
                float y_ScaleRatio = 0;

                //Масштабируемые пиксели
                uint x_Scalable = 0;
                uint y_Scalable = 0;

                //Определение границ цифры
                //Проход по матрице с до нахождения первого нефонового пикселя

                //Левый край
                for ( x = 0; x < X_border; ++x )
                {
                    for ( y = 0; ( y < Y_border ) && ( !pixels[ y, x ] ); ++y )
                        ;

                    if ( y < Y_border )
                        break;
                }
                LeftFigureBorder = x;

                //Правый край 
                for ( x = X_border - 1; x >= 0; --x )
                {
                    for ( y = 0; ( y < Y_border ) && ( !pixels[ y, x ] ); ++y )
                        ;

                    if ( y < Y_border )
                        break;
                }
                RightFigureBorder = x;

                //Нижний край
                for ( y = 0; y < Y_border; ++y )
                {
                    for ( x = 0; ( x < X_border ) && ( !pixels[ y, x ] ); ++x )
                        ;

                    if ( x < X_border )
                        break;
                }
                BotFigureBorder = y;

                //Верхний край
                for ( y = Y_border - 1; y >= 0; --y )
                {
                    for ( x = 0; ( x < X_border ) && ( !pixels[ y, x ] ); ++x )
                        ;

                    if ( x < X_border )
                        break;
                }
                TopFigureBorder = y;

                //Определение коэффициентов масштабирования
                x_ScaleRatio = ( ( float ) X_border ) / ( ( float ) ( RightFigureBorder - LeftFigureBorder + 1 ) );
                y_ScaleRatio = ( ( float ) Y_border ) / ( ( float ) ( TopFigureBorder - BotFigureBorder + 1 ) );

                //Масштабирование(нормирование) изображения в его образ
                for ( y = 0; y < Y_border; ++y )
                {
                    for ( x = 0; x < X_border; ++x )
                    {
                        //Вычисление координат пикселя в масштабе всего изображения
                        y_Scalable = ( uint ) ( BotFigureBorder + ( ( float ) y ) / ( ( float ) y_ScaleRatio ) );
                        x_Scalable = ( uint ) ( LeftFigureBorder + ( ( float ) x ) / ( ( float ) x_ScaleRatio ) );

                        //Если пиксель выходит за границы фигуры
                        if ( x_Scalable < LeftFigureBorder )
                            x_Scalable = LeftFigureBorder;

                        if ( x_Scalable > RightFigureBorder )
                            x_Scalable = RightFigureBorder;


                        if ( y_Scalable < BotFigureBorder )
                            y_Scalable = BotFigureBorder;

                        if ( y_Scalable > TopFigureBorder )
                            y_Scalable = TopFigureBorder;

                        //Задание пикселя в образе
                        obraz_matrix[ y, x ] = pixels[ y_Scalable, x_Scalable ];
                    }
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при масштабировании образа.", "Образ " + Obraz_File_Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }
        /// <summary>
        /// Каталог для хранения образов по умолчанию
        /// </summary>
        private const String DefaultCatalog = "\\decisions\\";

        /// <summary>
        /// Приписка к имени файлов образов
        /// </summary>
        private const String DefaultObrazFileNameAddition = "_obraz.txt";

        /// <summary>
        /// Получение имя образа по умолчанию для заданного имени изображения
        /// </summary>
        /// <param name="FileName">Имя изображения</param>
        /// <returns></returns>
        public static String GetDefaultObrazName( string ImageName )
        {
            return ImageName + DefaultObrazFileNameAddition;
        }

        /// <summary>
        /// Запись образа в лог-файл
        /// </summary>
        /// <param name="ImageFileName">Имя файла изображения</param>
        /// <param name="AdditionalCatalog">Подкаталог корневой папки, в который ведётся запись</param>
        public void WriteObrazLog( String ImageFileName, String AdditionalCatalog = DefaultCatalog )
        {
            //Создание папки, если она отсутствует
            if ( !Directory.Exists( Directory.GetCurrentDirectory() + AdditionalCatalog ) )
                Directory.CreateDirectory( Directory.GetCurrentDirectory() + AdditionalCatalog );

            String LogFilePath = ( Directory.GetCurrentDirectory() + AdditionalCatalog + GetDefaultObrazName( ImageFileName ) );

            using ( StreamWriter ObrazLogFile = new StreamWriter( LogFilePath ) )
                try
                {
                    for ( uint y = 0; y < Y_border; ++y )
                    {
                        for ( uint x = 0; x < X_border; ++x )
                        {
                            if ( obraz_matrix[ y, x ] )
                                ObrazLogFile.Write( '1' );
                            else
                                ObrazLogFile.Write( '0' );
                        }
                        ObrazLogFile.WriteLine();
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при записи в файл.", "Образ " + Obraz_File_Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
        }

        /// <summary>
        /// Загрузка образа из файла
        /// </summary>
        /// <param name="ObrazFileName">Путь к файлу образа</param>
        public void LoadObrazFromFile( String ObrazFilePath )
        {
            using ( StreamReader ObrazFile = new StreamReader( ObrazFilePath ) )
                try
                {
                    Obraz_File_Name = Path.GetFileNameWithoutExtension( ObrazFilePath );

                    //Чтение образа в одну строку
                    String SummaryString = ObrazFile.ReadToEnd();

                    //Определение горизонтальной границы

                    X_border = ( uint ) SummaryString.IndexOf( Environment.NewLine );

                    //Определение вертикальной границы
                    SummaryString = SummaryString.Replace( Environment.NewLine, "" ); //Убирает все переносы строки

                    Y_border = ( uint ) SummaryString.Length / X_border;

                    //Создание матрицы образа и её заполнение
                    obraz_matrix = new bool[ Y_border, X_border ];

                    for ( uint Y_coord = 0; Y_coord < Y_border; ++Y_coord )
                    {
                        for ( uint X_coord = 0; X_coord < X_border; ++X_coord )
                        {
                            char CurrentChar = SummaryString[ ( int ) ( Y_coord * X_border + X_coord ) ];
                            obraz_matrix[ Y_coord, X_coord ] = Convert.ToBoolean( CurrentChar - '0' );
                        }
                    }

                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при загрузке из файла.", "Образ " + Obraz_File_Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
        }
    }
}
