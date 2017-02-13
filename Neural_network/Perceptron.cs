using System;

using System.Windows.Forms; //Для использования класса MessageBox и т.д.
using System.IO; //Для чтения/записи в файл
using System.Collections; //Для подключения интерфейса IEnumerable

namespace PerceptronNS
{
    /// <summary>
    /// Перечисление, содержащее исследуемые фигуры
    /// </summary>
    enum FigureType : uint
    {
        Digit_0 = 0,
        Digit_1 = 1,
        Digit_2 = 2,
        Digit_3 = 3,
        Digit_4 = 4,
        Digit_5 = 5,
        Digit_6 = 6,
        Digit_7 = 7,
        Digit_8 = 8,
        Digit_9 = 9,
    }

    class Perceptron
    {
        /// <summary>
        /// Длина соответствующих векторов
        /// </summary>
        public uint Vector_Length { get; private set; }

        /// <summary>
        /// Количество нейронов в персептроне
        /// </summary>
        public uint Neurons_Amount { get; private set; } //Количеству нейронов соответствует количество столбцов в матрице ответов

        /// <summary>
        /// Количество распознаваемых фигур
        /// </summary>
        public uint Figures_Amount { get; private set; }

        /// <summary>
        /// Матрица весов компонент векторов каждого персептрона
        /// </summary>
        public int[,] weights_matrix { get; private set; }

        /// <summary>
        /// Матрица ответов, содержащая уникальный код каждой фигуры
        /// </summary>
        public int[,] answer_matrix { get; private set; }

        /// <summary>
        /// Массив, содержащий типы фигур
        /// </summary>
        public FigureType[] figures_array { get; private set; }

        /// <summary>
        /// Создание персептрона нейронной сети
        /// </summary>
        /// <param name="Vect_len">Длина соответствуюего полного вектора</param>
        public Perceptron( uint Vect_len )
        {
            Vector_Length = Vect_len;

            //Получение массива фигур
            figures_array = ( FigureType[] ) Enum.GetValues( typeof( FigureType ) );

            Figures_Amount = ( uint ) figures_array.Length;

            //Получение числа нейронов, необходимых для работы
            Neurons_Amount = GetNeuronsAmount();

            //Обнуление матрицы ответов
            answer_matrix = new int[ Figures_Amount, Neurons_Amount ];

            ResetAnswerMatrix();

            //Обнуление матрицы весов
            weights_matrix = new int[ Vector_Length, Neurons_Amount ];

            ResetWeightsMatrix();
        }

        /// <summary>
        /// Конструктор пустого персептрона
        /// </summary>
        public Perceptron()
        {
            Vector_Length = 0;

            //Получение массива фигур
            figures_array = ( FigureType[] ) Enum.GetValues( typeof( FigureType ) );

            Figures_Amount = ( uint ) figures_array.Length;

            //Получение числа нейронов, необходимых для работы
            Neurons_Amount = GetNeuronsAmount();

            //Обнуление матрицы ответов
            answer_matrix = new int[ Figures_Amount, Neurons_Amount ];

            ResetAnswerMatrix();

            //Обнуление матрицы весов
            weights_matrix = null;
        }

        /// <summary>
        /// Задаёт новую длину полного вектора
        /// Обнуляет матрицу весов персептрона
        /// </summary>
        /// <param name="VecLen">Длина полного вектора</param>
        public void SetVectorLength( uint VecLen )
        {
            Vector_Length = VecLen;

            if ( Vector_Length == 0 )
            {
                weights_matrix = null;
            }
            else
            {
                //Обнуление матрицы весов
                weights_matrix = new int[ Vector_Length, Neurons_Amount ];

                ResetWeightsMatrix();
            }
        }

        /// <summary>
        /// Обнуление матрицы ответов
        /// </summary>
        public void ResetAnswerMatrix()
        {
            try
            {
                answer_matrix = new int[ Figures_Amount, Neurons_Amount ];

                for ( uint CurrentFigure = 0; CurrentFigure < Figures_Amount; ++CurrentFigure )
                {
                    for ( uint CurrentComponent = 0; CurrentComponent < Neurons_Amount; ++CurrentComponent )
                    {
                        answer_matrix[ CurrentFigure, CurrentComponent ] = 0;
                    }
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при обнулении матрицы ответов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

        }

        /// <summary>
        /// Обнуление матрицы весов
        /// </summary>
        public void ResetWeightsMatrix()
        {
            try
            {
                weights_matrix = new int[ Vector_Length, Neurons_Amount ];

                for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                {
                    for ( uint CurrentNeuron = 0; CurrentNeuron < Neurons_Amount; ++CurrentNeuron )
                    {
                        weights_matrix[ VectorComponent, CurrentNeuron ] = 0;
                    }
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при обнулении матрицы весов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Получение количества нейронов, необходимых для кодирования фигур
        /// </summary>
        /// <returns></returns>
        private uint GetNeuronsAmount()
        {
            try
            {
                uint MaxCodeLength = 0; //Максимальная длина кода фигуры 
                //(минимальная необходимая для однозначного кодирования всех фигур - сответствует количеству нейронов)

                FigureType MaxCodeFigure = figures_array[ 0 ]; //Фигура, которой соответствует максимальная длина кода

                for ( uint CurrentFigure = 0; CurrentFigure < Figures_Amount; ++CurrentFigure ) //Вычисление фигуры с максимальным соответствующим значением
                {
                    if ( figures_array[ CurrentFigure ] > MaxCodeFigure )
                        MaxCodeFigure = figures_array[ CurrentFigure ];
                }

                uint CorrespondingNumber = ( uint ) MaxCodeFigure; //Число, соответствующее фигуре с максимальным кодом

                uint TwoDegree = 0; //Текущая степень двойки
                uint TwoInDegree = 1; //Двойка в текущей степени

                while ( CorrespondingNumber >= TwoInDegree )
                {
                    ++TwoDegree;

                    TwoInDegree = ( uint ) Math.Pow( 2, TwoDegree );
                }

                MaxCodeLength = TwoDegree;

                return MaxCodeLength;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при вычислении необходимого количества нейронов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return 0;
            }
        }

        /// <summary>
        /// Загрузка матрицы ответов из файла
        /// </summary>
        /// <param name="MatrixFilePath">Полный путь к матрице ответов в файле</param>
        public void LoadAnswerMatrix( String AnswerMatrixFilePath )
        {
            using ( StreamReader AnswerMatrixFile = new StreamReader( AnswerMatrixFilePath ) )
                try
                {
                    for ( uint CurrentFigure = 0; CurrentFigure < Figures_Amount; ++CurrentFigure )
                    {
                        String FigureComponentsLine = AnswerMatrixFile.ReadLine();  //Строка, содержащая компоненты текущей фигуры
                        char Symbol; //Текущий ситываемый символ из строки

                        //Проверка на совпадение количества компонент в строке
                        if ( FigureComponentsLine.Length != Neurons_Amount )
                            throw new Exception();

                        for ( uint CurrentComponent = 0; CurrentComponent < Neurons_Amount; ++CurrentComponent )
                        {
                            Symbol = FigureComponentsLine[ ( int ) CurrentComponent ]; //Каждая компонента - одна цифра

                            answer_matrix[ CurrentFigure, CurrentComponent ] = Symbol - '0'; //Запись текущей компоненты
                        }

                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при считывании матрицы ответов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    //return;
                    throw;
                }
        }

        /// <summary>
        /// Создать уникальный код для заданной фигуры
        /// </summary>
        /// <param name="CurrentFigure">Заданная фигура</param>
        /// <returns>Массив компонент кода одной фигуры</returns>
        private Byte[] GenerateFigureCode( FigureType CurrentFigure )
        {
            try
            {
                Byte[] ComponentsArray = new Byte[ Neurons_Amount ]; //Массив компонент кода

                uint CurrentComponent = 0; //Номер компоненты для записи
                for ( CurrentComponent = 0; CurrentComponent < Neurons_Amount; ++CurrentComponent ) //Обнуление массива кода
                    ComponentsArray[ CurrentComponent ] = 0;

                uint CorrespondingNumber = ( uint ) CurrentFigure; //Число, соответствующее данной фигуре

                uint TwoDegree = 0; //Текущая степень двойки
                uint TwoInDegree = 1; //Двойка в текущей степени
                CurrentComponent = Neurons_Amount - 1;

                //Разложение числа по степеням двойки
                while ( CorrespondingNumber >= TwoInDegree )
                {
                    if ( Convert.ToBoolean( CorrespondingNumber & TwoInDegree ) )
                    {
                        ComponentsArray[ CurrentComponent ] = 1; //Запись в конец
                    }

                    ++TwoDegree; //Увеличение степени двойки
                    TwoInDegree = ( uint ) Math.Pow( 2, TwoDegree );
                    --CurrentComponent;
                }

                return ComponentsArray;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при создании уникального кода для фигуры " + CurrentFigure + ".", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return null;
            }
        }

        /// <summary>
        /// Имя матрицы ответов по умолчанию
        /// </summary>
        public const String DefaultAnswerMatrixFileName = "AnswerGen.perc";

        /// <summary>
        /// Сгенерировать файл с матрицей ответов
        /// </summary>
        public void GenerateAnswerMatrixFile()
        {
            String AnswerMatrixFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultAnswerMatrixFileName;

            using ( StreamWriter AnswerMatrixFile = new StreamWriter( AnswerMatrixFilePath ) )
                try
                {
                    //Для каждой фигуры создается уникальный вектор из <количество нейронов> компонент
                    for ( uint CurrentFigure = 0; CurrentFigure < Figures_Amount; ++CurrentFigure )
                    {
                        Byte[] CurrentCode = GenerateFigureCode( figures_array[ CurrentFigure ] ); //Создание кода для текущей фигуры

                        //Запись кода текущей фигуры
                        for ( uint CurrentComponent = 0; CurrentComponent < Neurons_Amount; ++CurrentComponent )
                        {
                            AnswerMatrixFile.Write( CurrentCode[ CurrentComponent ] );
                        }

                        AnswerMatrixFile.WriteLine();
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при создании файла матрицы ответов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
        }

        /// <summary>
        /// Загрузка матрицы весов из файла
        /// </summary>
        /// <param name="AnswerMatrixFilePath">Полный путь к матрице весов в файле</param>
        public void LoadWeightsMatrix( String WeightsMatrixFilePath )
        {
            using ( StreamReader WeightsMatrixFile = new StreamReader( WeightsMatrixFilePath ) )
                try
                {
                    for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                    {
                        String WeightsMatrixComponentsLine = WeightsMatrixFile.ReadLine(); //Строка текущих компонент для всех нейронов (разделены запятыми)
                        char[] separators = new char[] { '.', ',', ' ', '\n', '\t', '\r' }; //Массив возможных разделителей в файле матрицы весов

                        String[] SeparatedComponents = WeightsMatrixComponentsLine.Split( separators, StringSplitOptions.RemoveEmptyEntries );

                        //Проверка на совпадение количества компонент в строке
                        if ( SeparatedComponents.Length != Neurons_Amount )
                            throw new Exception();

                        for ( uint CurrentNeuron = 0; CurrentNeuron < Neurons_Amount; ++CurrentNeuron )
                        {
                            weights_matrix[ VectorComponent, CurrentNeuron ] = Convert.ToInt32( SeparatedComponents[ ( int ) CurrentNeuron ] ); //Запись в матрицу соответствующей компоненты
                        }
                    }

                    //Если это не конец файла
                    if ( !WeightsMatrixFile.EndOfStream )
                    {
                        throw new Exception();
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Не удалось загрузить матрицу весов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                    //return;
                    throw;
                }
        }

        /// <summary>
        /// Имя матрицы весов по умолчанию
        /// </summary>
        public const String DefaultWeightsMatrixFileName = "WeightsSaved.perc";

        /// <summary>
        /// Сохранение матрицы весов
        /// </summary>
        public void SaveWeightsMatrix()
        {
            String WeightsMatrixFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultWeightsMatrixFileName;

            using ( StreamWriter WeightsMatrixFile = new StreamWriter( WeightsMatrixFilePath ) )
                try
                {
                    for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                    {
                        for ( uint CurrentNeuron = 0; CurrentNeuron < Neurons_Amount; ++CurrentNeuron )
                        {
                            WeightsMatrixFile.Write( weights_matrix[ VectorComponent, CurrentNeuron ] ); //Запись компоненты 

                            if ( CurrentNeuron < ( Neurons_Amount - 1 ) )
                                WeightsMatrixFile.Write( ',' ); //Запись разделителя
                        }

                        WeightsMatrixFile.WriteLine(); //Переход на следующую строку
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Ошибка при сохранении матрицы весов.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
        }

        /// <summary>
        /// Получает выходной вектор персептрона
        /// </summary>
        /// <param name="InputVector">Входной вектор, соответствующий изображению фигуры</param>
        /// <returns></returns>
        private int[] GetOutputVector( int[] InputVector )
        {
            try
            {
                int[] OutputVector = new int[ Neurons_Amount ]; //Выходной вектор

                //Запись каждой компоненты выходного вектора
                for ( uint CurrentOutVectorComponent = 0; CurrentOutVectorComponent < Neurons_Amount; ++CurrentOutVectorComponent )
                {
                    OutputVector[ CurrentOutVectorComponent ] = 0;

                    //Получение компоненты выходного вектора
                    //Путем перемножения сответствующих компонент входного
                    //На соответствующие комоненты столбца матрицы весов
                    for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                    {
                        OutputVector[ CurrentOutVectorComponent ] += weights_matrix[ VectorComponent, CurrentOutVectorComponent ] * InputVector[ VectorComponent ];
                    }

                    //Корректировка компонент выходного вектора
                    //К состояниям 0 и 1
                    if ( OutputVector[ CurrentOutVectorComponent ] >= 0 )
                        OutputVector[ CurrentOutVectorComponent ] = 1;
                    else
                        OutputVector[ CurrentOutVectorComponent ] = 0;
                }

                return OutputVector;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при создании выходного вектора персептрона.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return null;
            }

        }

        /// <summary>
        /// Обучает персептрон распознаванию заданной фигуры
        /// </summary>
        /// <param name="InputVector">Входной вектор, соответствующий изображению фигуры</param>
        /// <param name="CurrentFigure">Тип распознаваемой фигуры</param>
        /// <returns>Успешно ли обучение</returns>
        public bool TeachPerceptronOnFigure( int[] InputVector, FigureType CurrentFigure )
        {
            try
            {
                bool TeachingSuccess = true; //Успешно ли обучение
                int[] OutputVector = GetOutputVector( InputVector ); //Выходной вектор, определяет решение персептрона

                //Сравниваем покомпонентно выходной вектор с соответствующим вектором в матрице ответов
                for ( uint CurrentOutVectorComponent = 0; CurrentOutVectorComponent < Neurons_Amount; ++CurrentOutVectorComponent )
                {
                    //Если компонента выходного вектора больше соответствующей компоненты вектора в матрице ответов
                    if ( OutputVector[ CurrentOutVectorComponent ] - answer_matrix[ ( uint ) CurrentFigure, CurrentOutVectorComponent ] > 0 )
                    {
                        TeachingSuccess = false; //Обучение не завершено

                        //Корректировка матрицы весов для соответствующего нейрона
                        //Уменьшаем веса в соответствующем столбце матрицы весов
                        for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                        {
                            weights_matrix[ VectorComponent, CurrentOutVectorComponent ] -= InputVector[ VectorComponent ];
                        }
                    }

                    //Если компонента выходного вектора меньше соответствующей компоненты вектора в матрице ответов
                    if ( OutputVector[ CurrentOutVectorComponent ] - answer_matrix[ ( uint ) CurrentFigure, CurrentOutVectorComponent ] < 0 )
                    {
                        TeachingSuccess = false; //Обучение не завершено

                        //Корректировка матрицы весов для соответствующего нейрона
                        //Увеличиваем веса в соответствующем столбце матрицы весов
                        for ( uint VectorComponent = 0; VectorComponent < Vector_Length; ++VectorComponent )
                        {
                            weights_matrix[ VectorComponent, CurrentOutVectorComponent ] += InputVector[ VectorComponent ];
                        }
                    }
                }

                return TeachingSuccess;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при обучении персептрона на фигуре " + CurrentFigure + ".", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }
        }

        /// <summary>
        /// Соответствует отсутствию ответа у персептрона
        /// </summary>
        private const String NO_ANSWER = "-";

        /// <summary>
        /// Соответствует ошибке при получении ответа
        /// </summary>
        private const String ERROR_ANSWER = "ERROR";

        /// <summary>
        /// Получает ответ персептрона о типе фигуры, основываясь на векторе, соответствующем ее изображению
        /// </summary>
        /// <param name="InputVector">Входной вектор, соответствующий изображению фигуры</param>
        /// <returns>Ответ персептрона в виде строки</returns>
        public String GetAnswerFromVector( int[] InputVector )
        {
            String PerceptronAnswer = NO_ANSWER;

            try
            {
                int[] OutputVector = GetOutputVector( InputVector );

                //Сравниваем выходной вектор со всеми векторами матрицы ответов
                for ( uint CurrentFigure = 0; CurrentFigure < Figures_Amount; ++CurrentFigure )
                {
                    uint CurrentOutVectorComponent = 0; //Текущая сравниваемая компонента вектора

                    //Покомпонентное сравнение векторов
                    for ( CurrentOutVectorComponent = 0; CurrentOutVectorComponent < Neurons_Amount; ++CurrentOutVectorComponent )
                    {
                        if ( OutputVector[ CurrentOutVectorComponent ] != answer_matrix[ CurrentFigure, CurrentOutVectorComponent ] )
                            break;
                    }

                    //Если все компоненты совпали
                    if ( CurrentOutVectorComponent == Neurons_Amount )
                    {
                        return PerceptronAnswer = CurrentFigure.ToString();
                    }
                }

                return PerceptronAnswer;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при получении ответа.", "Персептрон", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return PerceptronAnswer = ERROR_ANSWER;
            }
        }
    }
}
