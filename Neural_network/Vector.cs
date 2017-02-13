using System;

using System.Windows.Forms; //Для использования класса MessageBox и т.д.
using System.IO; //Для чтения/записи в файл

using ObrazNS;

namespace VectorNS
{
    /// <summary>
    /// Структура, содержащая все компоненты вектора
    /// </summary>
    struct vector_struct
    {
        public int[] RectangleComponent;
        public int[] HorLinesComponent;
        public int[] VertLinesComponent;
    };

    /// <summary>
    /// Класс, содержащий и задающий вектор признаков изображения
    /// </summary>
    class Vector
    {
        /// <summary>
        /// Вектор в виде структуры (совокупности его компонент)
        /// </summary>
        private vector_struct vector_components;

        /// <summary>
        /// Суммарный (полный) вектор образа
        /// </summary>
        public int[] complete_vector;

        /// <summary>
        /// Количество разбиений образа по вертикальной оси
        /// </summary>
        public uint Y_fragmentation { get; private set; }

        /// <summary>
        /// Количество разбиений образа по горизонтальной оси
        /// </summary>
        public uint X_fragmentation { get; private set; }

        /// <summary>
        /// Размер соответствующего образа по вертикальной оси
        /// </summary>
        private uint Y_border;

        /// <summary>
        /// Размер cответствующего образа по горизонтальной оси
        /// </summary>
        private uint X_border;

        /// <summary>
        /// Булева матрица соответствующего образа
        /// </summary>
        private bool[,] obraz_matrix;

        /// <summary>
        /// Получение вектора признаков
        /// </summary>
        /// <param name="CorrespondingObraz">Соответствующий образ</param>
        /// <param name="Y_fragment">Количество разбиений образа по вертикальной оси</param>
        /// <param name="X_fragment">Количество разбиений образа по горизонтальной оси</param>
        public Vector( Obraz CorrespondingObraz, uint Y_fragment, uint X_fragment )
        {
            obraz_matrix = CorrespondingObraz.obraz_matrix;

            Y_border = CorrespondingObraz.Y_border;
            X_border = CorrespondingObraz.X_border;

            Y_fragmentation = Y_fragment;
            X_fragmentation = X_fragment;

            //Задание компоненты вектора (разбиение на прямоугольники)
            RectangleFragmentation();

            //Задание компоненты вектора (разбиение горизонтальными линиями)
            HorLinesFragmentation();

            //Задание компоненты вектора (разбиение вертикальными линиями)
            VertLinesFragmentation();

            //Получение суммарного (полного) вектора
            SetCompleteVector();

            //WriteVectorLog(CorrespondingObraz.Obraz_File_Name);

        }

        /// <summary>
        /// Конструктор пустого вектора
        /// </summary>
        public Vector()
        {
            obraz_matrix = null;
            complete_vector = null;

            Y_border = 0;
            X_border = 0;

            Y_fragmentation = 0;
            X_fragmentation = 0;
        }

        /// <summary>
        /// Получение коэффициента значимости нефоновых пикселей
        /// </summary>
        /// <returns></returns>
        private double GetColorImportanceRatio()
        {
            try
            {
                double FontPixels = 0; //Количество фоновых пикселей
                double FigurePixels = 0; //Количество нефоновых пикселей

                //Подсчёт соответствующих пикселей
                for ( uint y = 0; y < Y_border; ++y )
                {
                    for ( uint x = 0; x < X_border; ++x )
                    {
                        if ( obraz_matrix[ y, x ] )
                            ++FigurePixels;
                        else
                            ++FontPixels;
                    }
                }

                return Math.Round( FontPixels / FigurePixels, 2 ); //Достаточно округления до 2 символов
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при вычислении коэффициента значимости нефонового цвета.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return 0;
            }
        }

        /// <summary>
        /// Получает составляющую вектора разбиением образа на прямоугольники
        /// </summary>
        private void RectangleFragmentation()
        {
            try
            {
                uint RectangleComponentLength = Y_fragmentation * X_fragmentation; //Задание длины компоненты вектора
                vector_components.RectangleComponent = new int[ RectangleComponentLength ];

                uint VertRectangleSide = Y_border / Y_fragmentation; //Вертикальная сторона прямоугольника
                uint HorizRectangleSide = X_border / X_fragmentation; //Горизонтальная сторона прямоугольника

                double ColorImportanceRatio = GetColorImportanceRatio(); //Коэффициент значимости нефоновых пикселей

                //Проход по прямоугольникам
                for ( uint RectangleRow = 0; RectangleRow < Y_fragmentation; ++RectangleRow ) //Текущий ряд прямоугольников
                {
                    for ( uint RectangleColumn = 0; RectangleColumn < X_fragmentation; ++RectangleColumn ) //Текущий столбец прямоугольников
                    {
                        uint FontPixels = 0; //Количество фоновых пикселей
                        uint FigurePixels = 0; //Количество нефоновых пикселей

                        //Проход по пикселям в каждом прямоугольнике
                        for ( uint VerticalPixel = 0; VerticalPixel < VertRectangleSide; ++VerticalPixel )
                        {
                            for ( uint HorizontalPixel = 0; HorizontalPixel < HorizRectangleSide; ++HorizontalPixel )
                            {
                                //Сторона прямоугольника по вертикали*Количество пройденных рядов + пиксель по вертикали, сторона прямоугольника по вертикали*количество пройденных столбцов + пиксель по горизонтали
                                if ( obraz_matrix[ VertRectangleSide * RectangleRow + VerticalPixel, HorizRectangleSide * RectangleColumn + HorizontalPixel ] )
                                    ++FigurePixels;
                                else
                                    ++FontPixels;

                            }
                        }

                        //Задание компоненты вектора
                        if ( ColorImportanceRatio * FigurePixels >= FontPixels )
                        {
                            //Количество прямоугольников в ряду*Количество пройденных рядов + текущее положение в ряду
                            vector_components.RectangleComponent[ X_fragmentation * RectangleRow + RectangleColumn ] = 1;
                        }
                        else
                        {
                            //Количество прямоугольников в ряду*Количество пройденных рядов + текущее положение в ряду
                            vector_components.RectangleComponent[ X_fragmentation * RectangleRow + RectangleColumn ] = -1;
                        }

                    }
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при разбиении на прямоугольники.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

        }

        /// <summary>
        /// Получает составляющую вектора разбиением образа горизонтальными линиями
        /// </summary>
        private void HorLinesFragmentation()
        {
            try
            {
                vector_components.HorLinesComponent = new int[ Y_fragmentation ];

                uint HorLinesFragmentation = ( Y_border / ( Y_fragmentation + 1 ) ); //Определяет позицию разбивающей линии от начала координат
                uint BorderIntersectionCount = 0; //Количество пересечений границ фигуры

                //Проход по всем горизонтальным линиям
                for ( uint CurrentLine = 1; CurrentLine <= Y_fragmentation; ++CurrentLine )
                {
                    if ( obraz_matrix[ CurrentLine * HorLinesFragmentation, 0 ] ) //Если фигура начинается с края изображения
                        BorderIntersectionCount = 1;
                    else
                        BorderIntersectionCount = 0;

                    for ( uint ObrazColumn = 1; ObrazColumn < X_border; ++ObrazColumn )
                    {
                        if ( ( obraz_matrix[ CurrentLine * HorLinesFragmentation, ObrazColumn ] ) && !( obraz_matrix[ CurrentLine * HorLinesFragmentation, ObrazColumn - 1 ] ) ) //Если переход с пикселей фона на пиксель фигуры
                            ++BorderIntersectionCount;
                    }

                    if ( BorderIntersectionCount > 1 )
                        vector_components.HorLinesComponent[ CurrentLine - 1 ] = 1;
                    else
                        vector_components.HorLinesComponent[ CurrentLine - 1 ] = -1;

                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при разбиении горизонтальными линиями.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Получает составляющую вектора разбиением образа вертикальными линиями
        /// </summary>
        private void VertLinesFragmentation()
        {
            try
            {
                vector_components.VertLinesComponent = new int[ X_fragmentation ];

                uint VertLinesFragmentation = ( X_border / ( X_fragmentation + 1 ) ); //Определяет позицию разбивающей линии от начала координат
                uint BorderIntersectionCount = 0; //Количество пересечений границ фигуры

                //Проход по всем вертикальным линиям
                for ( uint CurrentLine = 1; CurrentLine <= X_fragmentation; ++CurrentLine )
                {
                    if ( obraz_matrix[ 0, CurrentLine * VertLinesFragmentation ] ) //Если фигура начинается с края изображения
                        BorderIntersectionCount = 1;
                    else
                        BorderIntersectionCount = 0;

                    for ( uint ObrazLine = 1; ObrazLine < Y_border; ++ObrazLine )
                    {
                        if ( ( obraz_matrix[ ObrazLine, CurrentLine * VertLinesFragmentation ] ) && !( obraz_matrix[ ObrazLine - 1, CurrentLine * VertLinesFragmentation ] ) ) //Если переход с пикселя фона на пиксель фигуры
                            ++BorderIntersectionCount;
                    }

                    if ( BorderIntersectionCount > 1 )
                        vector_components.VertLinesComponent[ CurrentLine - 1 ] = 1;
                    else
                        vector_components.VertLinesComponent[ CurrentLine - 1 ] = -1;

                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при разбиении вертикальными линиями.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

        }

        /// <summary>
        /// Получает суммарную длину вектора для изображений
        /// </summary>
        /// <param name="Y_fragmentation">Разбиение вертикальной оси</param>
        /// <param name="X_fragmentation">Разбиение горизонтальной оси</param>
        /// <returns></returns>
        public static uint GetCompleteVectorLength( uint Y_fragmentation, uint X_fragmentation )
        {
            return X_fragmentation * Y_fragmentation + X_fragmentation + Y_fragmentation;
        }

        /// <summary>
        ///  Задаёт соответствующее свойство: вектор как единый массив
        /// </summary>
        private void SetCompleteVector()
        {
            try
            {
                uint CompleteVectorLength = GetCompleteVectorLength( Y_fragmentation, X_fragmentation );

                complete_vector = new int[ CompleteVectorLength ];

                uint RectangleComponentsLength = X_fragmentation * Y_fragmentation; //Длина компоненты разбиений по прямоугольникам
                for ( uint CurrentComponent = 0; CurrentComponent < RectangleComponentsLength; ++CurrentComponent ) //Запись компоненты разбиений по прямоугольникам
                {
                    complete_vector[ CurrentComponent ] = vector_components.RectangleComponent[ CurrentComponent ];
                }

                ////////////////////////

                for ( uint CurrentComponent = 0; CurrentComponent < Y_fragmentation; ++CurrentComponent ) //Запись компоненты разбиений горизонтальными линиями
                {
                    complete_vector[ CurrentComponent + RectangleComponentsLength ] = vector_components.HorLinesComponent[ CurrentComponent ];
                }

                ////////////////////////

                for ( uint CurrentComponent = 0; CurrentComponent < X_fragmentation; ++CurrentComponent ) //Запись компоненты разбиений вертикальными линиями
                {
                    complete_vector[ CurrentComponent + RectangleComponentsLength + Y_fragmentation ] = vector_components.VertLinesComponent[ CurrentComponent ];
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при создании полного вектора", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Получает структуру, содержащую раздельно компоненты вектора
        /// </summary>
        /// <returns></returns>
        public vector_struct GetVectorComponents()
        {
            return vector_components;
        }

        /// <summary>
        /// Каталог для хранения образов по умолчанию
        /// </summary>
        private const String DefaultCatalog = "\\decisions\\";

        /// <summary>
        /// Приписка к имени файлов образов
        /// </summary>
        private const String DefaultVectorFileNameAddition = "_vector.txt";

        /// <summary>
        /// Получение имени вектора по умолчанию для заданного имени изображения
        /// </summary>
        /// <param name="ImageFileName">Имя изображения</param>
        /// <returns></returns>
        public static String GetDefaultVectorName( String ImageName )
        {
            return ImageName + DefaultVectorFileNameAddition;
        }

        /// <summary>
        /// Запись вектора в лог-файл
        /// </summary>
        /// <param name="ImageFileName">Имя файла изображения соответствующего образа</param>
        /// <param name="AdditionalCatalog">Подкаталог корневой папки, в который ведётся запись</param>
        public void WriteVectorLog( String ImageFileName, String AdditionalCatalog = DefaultCatalog )
        {
            //Создание папки, если она отсутствует
            if ( !Directory.Exists( Directory.GetCurrentDirectory() + AdditionalCatalog ) )
                Directory.CreateDirectory( Directory.GetCurrentDirectory() + AdditionalCatalog );

            String LogFilePath = ( Directory.GetCurrentDirectory() + AdditionalCatalog + GetDefaultVectorName( ImageFileName ) );

            using ( StreamWriter VectorLogFile = new StreamWriter( LogFilePath ) )
                try
                {
                    uint RectangleComponentLength = X_fragmentation * Y_fragmentation; //Суммарная длина компонент разбиений прямоугольниками

                    for ( uint CurrentComponent = 0; CurrentComponent < RectangleComponentLength; ++CurrentComponent ) //Запись компонент разбиений прямоугольниками
                    {
                        if ( vector_components.RectangleComponent[ CurrentComponent ] == 1 )
                            VectorLogFile.Write( 1 );
                        else
                            if ( vector_components.RectangleComponent[ CurrentComponent ] == -1 )
                            VectorLogFile.Write( 0 ); //Вместо -1 записывается 0 для удобства восприятия
                        else
                            VectorLogFile.Write( '?' );
                    }

                    VectorLogFile.Write( ' ' );

                    for ( uint CurrentComponent = 0; CurrentComponent < Y_fragmentation; ++CurrentComponent ) //Запись компонент разбиений горизонтальными линиями
                    {
                        if ( vector_components.HorLinesComponent[ CurrentComponent ] == 1 )
                            VectorLogFile.Write( 1 );
                        else
                            if ( vector_components.HorLinesComponent[ CurrentComponent ] == -1 )
                            VectorLogFile.Write( 0 ); //Вместо -1 записывается 0 для удобства восприятия
                        else
                            VectorLogFile.Write( '?' );
                    }

                    VectorLogFile.Write( ' ' );

                    for ( uint CurrentComponent = 0; CurrentComponent < X_fragmentation; ++CurrentComponent ) //Запись компонент разбиений вертикальными линиями
                    {
                        if ( vector_components.VertLinesComponent[ CurrentComponent ] == 1 )
                            VectorLogFile.Write( 1 );
                        else
                            if ( vector_components.VertLinesComponent[ CurrentComponent ] == -1 )
                            VectorLogFile.Write( 0 ); //Вместо -1 записывается 0 для удобства восприятия
                        else
                            VectorLogFile.Write( '?' );
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при записи в файл.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
        }

        /// <summary>
        /// Загрузка вектора из файла
        /// </summary>
        /// <param name="VectorFilePath">Путь к файлу вектора</param>
        /// <param name="Y_framgentation">Количество разбиений по Y</param>
        /// <param name="X_fragmentation">Количество разбиений по X</param>
        public void LoadVectorFromFile( String VectorFilePath, uint Y_fragment, uint X_fragment )
        {
            StreamReader VectorFile = null;

            try
            {
                //Задание количества разбиений
                Y_fragmentation = Y_fragment;
                X_fragmentation = X_fragment;

                //Инициализация компонент вектора

                uint RectangleComponentLength = Y_fragmentation * X_fragmentation; //Суммарная длина компонент разбиений прямоугольниками

                vector_components.RectangleComponent = new int[ RectangleComponentLength ];

                vector_components.HorLinesComponent = new int[ Y_fragmentation ];
                vector_components.VertLinesComponent = new int[ X_fragmentation ];



                VectorFile = new StreamReader( VectorFilePath );

                String VectorString = VectorFile.ReadToEnd(); //Строка, содержащая весь вектор
                VectorString = VectorString.Replace( " ", "" ); //Удаление пробелов

                String CurrentComponentsString = null; //Строка, содержащая текущую компоненту

                uint CurrentComponentNumber = 0; //Номер текущей подкомпоненты 

                //Считывание компонент разбиений прямоугольникам

                CurrentComponentsString = VectorString.Substring( 0, ( int ) RectangleComponentLength );

                foreach ( char ComponentChar in CurrentComponentsString )
                {
                    if ( ComponentChar == '1' )
                    {
                        vector_components.RectangleComponent[ CurrentComponentNumber ] = 1;
                        ++CurrentComponentNumber;
                        continue;
                    }


                    if ( ComponentChar == '0' )
                    {
                        vector_components.RectangleComponent[ CurrentComponentNumber ] = -1;
                        ++CurrentComponentNumber;
                        continue;
                    }

                    //Иначе - ошибка
                    throw new Exception();
                }

                //Считывание компонент разбиений горизонтальными линиями

                CurrentComponentsString = VectorString.Substring( ( int ) RectangleComponentLength, ( int ) Y_fragmentation );
                CurrentComponentNumber = 0;

                foreach ( char ComponentChar in CurrentComponentsString )
                {
                    if ( ComponentChar == '1' )
                    {
                        vector_components.HorLinesComponent[ CurrentComponentNumber ] = 1;
                        ++CurrentComponentNumber;
                        continue;
                    }


                    if ( ComponentChar == '0' )
                    {
                        vector_components.HorLinesComponent[ CurrentComponentNumber ] = -1;
                        ++CurrentComponentNumber;
                        continue;
                    }

                    //Иначе - ошибка
                    throw new Exception();

                }

                //Считывание компонент разбиений вертикальными линиями

                CurrentComponentsString = VectorString.Substring( ( int ) ( RectangleComponentLength + Y_fragmentation ), ( int ) X_fragmentation );
                CurrentComponentNumber = 0;

                foreach ( char ComponentChar in CurrentComponentsString )
                {
                    if ( ComponentChar == '1' )
                    {
                        vector_components.VertLinesComponent[ CurrentComponentNumber ] = 1;
                        ++CurrentComponentNumber;
                        continue;
                    }


                    if ( ComponentChar == '0' )
                    {
                        vector_components.VertLinesComponent[ CurrentComponentNumber ] = -1;
                        ++CurrentComponentNumber;
                        continue;
                    }

                    //Иначе - ошибка
                    throw new Exception();
                }

                //Задание полного вектора
                SetCompleteVector();


            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при загрузке из файла.", "Вектор", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            finally
            {
                if ( VectorFile != null )
                    VectorFile.Dispose();
            }
        }
    }
}


