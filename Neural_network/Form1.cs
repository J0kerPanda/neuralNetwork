using System;
using System.Windows.Forms;
using System.IO;
using System.Text; //Класс StringBuilder
using System.Linq;//Сортировка
using System.Collections;

using ObrazNS;
using VectorNS;
using PerceptronNS;

namespace Neural_network
{
    public partial class MainInterfaceForm : Form
    {
        /// <summary>
        /// Количество разбиений изображения по вертикальной оси
        /// </summary>
        private uint Y_fragmentation;

        /// <summary>
        /// Количество разбиений изображения по горизонтальной оси
        /// </summary>
        private uint X_fragmentation;

        /// <summary>
        /// Количество существующих обучающих наборов
        /// </summary>
        private uint TeachingSetsCount;

        /// <summary>
        /// Основной персептрон нейронной сети
        /// </summary>
        FormPerceptron MainPerceptron;

        /// <summary>
        /// Задаёт разбиения, исходя из данных в форме
        /// </summary>
        private void InitializeFragmentations()
        {
            try
            {
                X_fragmentation = Convert.ToUInt32( X_FragmentationTextBox.Text );
                Y_fragmentation = Convert.ToUInt32( Y_FragmentationTextBox.Text );
            }
            catch
            {
                MessageBox.Show( "Ошибка при задании количества разбиений.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Задание текущей длины вектора для главного персептрона.
        /// Обнуляет матрицу весов
        /// </summary>>
        /// <param name="Y_frag">Разбиения вертикальной составляющей</param>
        /// <param name="X_frag">Разбиения горизонтальной составляющей</param>
        /// <returns></returns>
        private void SetMainPerceptronVectorLength( uint Y_frag, uint X_frag )
        {
            try
            {
                uint VectorLength = Vector.GetCompleteVectorLength( Y_frag, X_frag );

                MainPerceptron.SetVectorLength( VectorLength );
            }
            catch
            {
                MessageBox.Show( "Ошибка при задании длины вектора персептрона.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Инициализация нейронной сети
        /// </summary>
        private void InitializeNeuralNetwork()
        {
            try
            {
                //Задание основного персептрона
                InitializeFragmentations();

                uint VectorLength = Vector.GetCompleteVectorLength( Y_fragmentation, X_fragmentation );

                MainPerceptron = new FormPerceptron( VectorLength );

                Neurons_AmountTextBox.Text = MainPerceptron.Neurons_Amount.ToString(); //Задание необходимого количества нейронов

                //Задание количества обучающих наборов
                SetTeachingSetsCount();

                //Задание их массива
                SetTeachingSetsFilePathsArray();

                //Попытки получения существующих матриц
                if ( MainPerceptron.TrySetWeightsMatrix() == true )
                    WeightsMatrixCheckBox.Checked = true;
                else
                    WeightsMatrixCheckBox.Checked = false;

                if ( MainPerceptron.TrySetAnswerMatrix() == true )
                    AnswerMatrixCheckBox.Checked = true;
                else
                    AnswerMatrixCheckBox.Checked = false;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при инициализации нейронной сети.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Расширение файлов изображений по умолчанию
        /// </summary>
        private const String DefaultImageExtension = ".bmp";

        /// <summary>
        /// Получает имя файла по умолчанию в обучающем наборе для заданной фигуры c указанием расширения
        /// </summary>
        /// <param name="CurrentFigure">Фигура</param>
        /// <param name="CurrentTeachingSet">Текущий обучающий набор</param>
        /// <returns></returns>
        private static String GetDefaultTeachingSetFileName( FigureType CurrentFigure, uint CurrentTeachingSet )
        {
            return CurrentTeachingSet.ToString() + "_" + ( ( uint ) CurrentFigure ).ToString() + DefaultImageExtension;
        }

        /// <summary>
        /// Каталог по умолчанию для хранения обучающих наборов
        /// </summary>
        private const String DefaultTeachingSetsCatalog = "\\teaching sets\\";

        /// <summary>
        /// Задаёт количество существующих обучающих наборов
        /// </summary>
        private void SetTeachingSetsCount()
        {
            try
            {
                String TeachingSetsDirectory = Directory.GetCurrentDirectory() + DefaultTeachingSetsCatalog;

                //Проверка существования директории
                if ( Directory.Exists( TeachingSetsDirectory ) )
                {
                    uint CurrentFigureIndex = 0; //Индекс, соответствующей текущей фигуре в массиве фигур

                    String CurrentFigureFilePath = TeachingSetsDirectory
                        //+ TeachingSetsCount.ToString() + "_"
                        + GetDefaultTeachingSetFileName( MainPerceptron.figures_array[ CurrentFigureIndex ], TeachingSetsCount ); //Путь к текущему файлу в виде строки 

                    TeachingSetsCount = 0;

                    //Проверка существования файлов для наборов
                    while ( File.Exists( CurrentFigureFilePath ) )
                    {
                        ++CurrentFigureIndex;

                        //Переход к новому набору
                        if ( CurrentFigureIndex == MainPerceptron.Figures_Amount )
                        {
                            CurrentFigureIndex = 0;

                            ++TeachingSetsCount;
                        }

                        CurrentFigureFilePath = TeachingSetsDirectory
                            //+ TeachingSetsCount.ToString() + "_"
                            + GetDefaultTeachingSetFileName( MainPerceptron.figures_array[ CurrentFigureIndex ], TeachingSetsCount );

                    }

                    TeachingSetsCountTextBox.Text = TeachingSetsCount.ToString();
                }
                else
                {
                    TeachingSetsCount = 0;

                    TeachingSetsCountTextBox.Text = TeachingSetsCount.ToString();

                    return;
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при получении количества обучающих наборов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Массив путей к файлам обучающих наборов, упорядоченный по имени
        /// </summary>
        private String[] TeachingSetsFilePaths;

        /// <summary>
        /// Задание массива путей к файлам обучающих наборов
        /// </summary>
        private void SetTeachingSetsFilePathsArray()
        {
            try
            {
                String TeachingSetsDirectory = Directory.GetCurrentDirectory() + DefaultTeachingSetsCatalog;

                if ( Directory.Exists( TeachingSetsDirectory ) )
                {

                    DirectoryInfo TeachingSetDirectoryInfo = new DirectoryInfo( Directory.GetCurrentDirectory() + DefaultTeachingSetsCatalog );

                    FileSystemInfo[] TeachingSetFilesInfo = TeachingSetDirectoryInfo.GetFileSystemInfos( "*" + DefaultImageExtension );

                    TeachingSetFilesInfo = ( ( FileSystemInfo[] ) TeachingSetFilesInfo.OrderBy( f => f.CreationTime ).ToArray() );

                    TeachingSetsFilePaths = new String[ TeachingSetsCount * MainPerceptron.Figures_Amount ]; //Пути к файлам всех обучающих наборов

                    for ( uint CurrentTeachingSet = 0; CurrentTeachingSet < TeachingSetsCount; ++CurrentTeachingSet ) //Проход по всем обучающим наборам
                    //foreach (String CurrentTeachingSetFile in TeachingSetFilesPaths)
                    {
                        for ( uint CurrentFigureIndex = 0; CurrentFigureIndex < MainPerceptron.Figures_Amount; ++CurrentFigureIndex ) //Проход по каждой фигуре в наборе
                        {
                            uint CurrentFileNumber = CurrentTeachingSet * MainPerceptron.Figures_Amount + CurrentFigureIndex;

                            TeachingSetsFilePaths[ CurrentFileNumber ] = TeachingSetFilesInfo[ CurrentFileNumber ].FullName;
                        }
                    }
                }
                else
                {
                    TeachingSetsFilePaths = null;
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при задания массива путей к файлам обучающих наборов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        private void DeleteTeachingSetsButton_Click( object sender, EventArgs e )
        {
            String TeachingSetsDirectoryPath = Directory.GetCurrentDirectory() + DefaultTeachingSetsCatalog;

            if ( Directory.Exists( TeachingSetsDirectoryPath ) )
                Directory.Delete( TeachingSetsDirectoryPath, true );

            TeachingSetsCount = 0;
            TeachingSetsCountTextBox.Text = TeachingSetsCount.ToString();
        }

        /////////////////////////////////////////////////////////////////////////////
        public MainInterfaceForm()
        {
            InitializeComponent();

            InitializeNeuralNetwork();
        }
        /////////////////////////////////////////////////////////////////////////////

        private void ResetWeightsMatrixButton_Click( object sender, EventArgs e )
        {
            try
            {
                InitializeFragmentations();

                SetMainPerceptronVectorLength( Y_fragmentation, X_fragmentation );

                MainPerceptron.SaveWeightsMatrix();

                TeachingDoneCheckBox.Checked = false;

                WeightsMatrixCheckBox.Checked = true;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при обнулении матрицы весов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        private void ResetAnswerMatrixButton_Click( object sender, EventArgs e )
        {
            try
            {
                String AnswerMatrixFilePath = Directory.GetCurrentDirectory() + "\\" + Perceptron.DefaultAnswerMatrixFileName;

                MainPerceptron.ResetAnswerMatrix();

                MainPerceptron.GenerateAnswerMatrixFile();

                MainPerceptron.LoadAnswerMatrix( AnswerMatrixFilePath );

                AnswerMatrixCheckBox.Checked = true;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при реинициализации матрицы ответов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        private void AddTeachingSetsButton_Click( object sender, EventArgs e )
        {
            try
            {
                OpenFileDialog AddTeachSetsFD = new OpenFileDialog(); //Файловый диалог для добавления файлов обучающих наборов
                AddTeachSetsFD.Filter = "Изображения " + DefaultImageExtension + "|*" + DefaultImageExtension; //"Файлы|*.bmp";
                AddTeachSetsFD.Multiselect = true;

                if ( AddTeachSetsFD.ShowDialog() == DialogResult.OK )
                {
                    String[] NewTeachingSetsFilePaths = AddTeachSetsFD.FileNames; //Содержит пути ко всем выбранным файлам

                    uint FileCount = ( uint ) NewTeachingSetsFilePaths.Length; //Количество добавляемых файлов

                    //Проверка, соответствует ли количество изображений необходимому
                    if ( FileCount % MainPerceptron.Figures_Amount != 0 )
                    {
                        MessageBox.Show( "В наборе необходимо минимум " + MainPerceptron.Figures_Amount + " файлов. Повторите выбор.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                        return;
                    }

                    //Проверка, существует ли каталог
                    String TeachingSetsDirectoryName = Directory.GetCurrentDirectory() + DefaultTeachingSetsCatalog;

                    //Если нет, то создать
                    if ( !Directory.Exists( TeachingSetsDirectoryName ) )
                    {
                        Directory.CreateDirectory( TeachingSetsDirectoryName );
                    }

                    uint CurrentFigureIndex = 0; //Индекс, соответствующей текущей добавляемой фигуре

                    uint CurrentSetIndex = TeachingSetsCount; //Индекс текущего добавляемого набора

                    //Добавление наборов в каталог (с уже форматированными именами)
                    for ( uint CurrentFileIndex = 0; CurrentFileIndex < FileCount; ++CurrentFileIndex )
                    {
                        String NewCurrentFilePath = TeachingSetsDirectoryName + Path.GetFileName( NewTeachingSetsFilePaths[ CurrentFileIndex ] ); //Новый путь текущего файла

                        File.Copy( NewTeachingSetsFilePaths[ CurrentFileIndex ], NewCurrentFilePath ); //Копирование файла в каталог обучающих наборов

                        String TeachSetFilePath = TeachingSetsDirectoryName
                            + GetDefaultTeachingSetFileName( MainPerceptron.figures_array[ CurrentFigureIndex ], TeachingSetsCount ); //Путь для файла, помещаемого в обучающий набор

                        File.Move( NewCurrentFilePath, TeachSetFilePath ); //Переименование файла

                        ++CurrentFigureIndex;

                        //Переход к новому набору
                        if ( CurrentFigureIndex == MainPerceptron.Figures_Amount )
                        {
                            CurrentFigureIndex = 0;
                            ++TeachingSetsCount;
                        }
                    }

                    TeachingSetsCountTextBox.Text = TeachingSetsCount.ToString();
                }

                SetTeachingSetsFilePathsArray();
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при добавлении обучающих наборов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
        }

        /// <summary>
        /// Каталог по умолчанию для хранения информации об обучающих наборах
        /// </summary>
        private const String DefaultTeachingSetsInformationCatalog = DefaultTeachingSetsCatalog + "decisions\\";

        /// <summary>
        /// Обучение на всех обучающих наборах
        /// </summary>
        /// <param name="Y_frag">Разбиения вертикальной составляющей</param>
        /// <param name="X_frag">Разбиения горизонтальной составляющей</param>
        /// <returns>Количество итераций, за которое произошло полное обучение</returns>
        private uint TeachOnAllTeachingSets( uint Y_frag, uint X_frag )
        {
            try
            {
                SetMainPerceptronVectorLength( Y_frag, X_frag );

                //Если не создан каталог для хранения информации об обучающих наборах
                String TeachingSetsInformationCatalogPath = Directory.GetCurrentDirectory() + DefaultTeachingSetsInformationCatalog;

                if ( !Directory.Exists( TeachingSetsInformationCatalogPath ) )
                    Directory.CreateDirectory( TeachingSetsInformationCatalogPath );


                uint TeachingSetsCount = Convert.ToUInt32( TeachingSetsCountTextBox.Text ); //Количество заданных обучающих наборов

                bool EveryTeachingSuccessful = false; //Прошло ли каждое обучение успешно

                uint IterationsAmount = 0; //Количество итераций


                //Цикл обучения
                while ( !EveryTeachingSuccessful )
                {
                    EveryTeachingSuccessful = true;

                    for ( uint CurrentTeachingSet = 0; CurrentTeachingSet < TeachingSetsCount; ++CurrentTeachingSet ) //Проход по всем обучающим наборам
                    {
                        for ( uint CurrentFigureIndex = 0; CurrentFigureIndex < MainPerceptron.Figures_Amount; ++CurrentFigureIndex ) //Проход по каждой фигуре в наборе
                        {
                            //Работа с образом

                            Obraz CurrentObraz = new Obraz(); //Образ текущего изображения

                            //Создание образов необходимо только на первом этапе для создания векторов
                            if ( IterationsAmount == 0 )
                            {
                                String ObrazName =
                                    Obraz.GetDefaultObrazName
                                    (
                                        Path.GetFileNameWithoutExtension
                                            (
                                                GetDefaultTeachingSetFileName( MainPerceptron.figures_array[ CurrentFigureIndex ], CurrentTeachingSet )
                                            )
                                    ); //Имя образа изображения

                                String CurrentFigureObrazFilePath = TeachingSetsInformationCatalogPath + ObrazName; //Путь к файлу текущего образа

                                if ( File.Exists( CurrentFigureObrazFilePath ) )  //Попытка получения уже существующего образа
                                {
                                    CurrentObraz.LoadObrazFromFile( CurrentFigureObrazFilePath );
                                }
                                else  //Создание нового образа
                                {
                                    String CurrentFigureFilePath = TeachingSetsFilePaths.ElementAt( ( int ) ( CurrentTeachingSet * MainPerceptron.Figures_Amount + CurrentFigureIndex ) ); //Путь к текущему файлу изображения

                                    CurrentObraz = new Obraz( CurrentFigureFilePath );

                                    CurrentObraz.WriteObrazLog( Path.GetFileNameWithoutExtension( CurrentFigureFilePath ), DefaultTeachingSetsInformationCatalog );
                                }
                            }

                            //Работа с вектором

                            Vector CurrentVector = new Vector();

                            //Попытка получения уже существующего вектора
                            if ( IterationsAmount != 0 )
                            {
                                String VectorName =
                                    Vector.GetDefaultVectorName
                                    (
                                        Path.GetFileNameWithoutExtension
                                        (
                                            GetDefaultTeachingSetFileName( MainPerceptron.figures_array[ CurrentFigureIndex ], CurrentTeachingSet )
                                        )
                                    ); //Имя вектора изображения

                                String CurrentFigureVectorPath = TeachingSetsInformationCatalogPath + VectorName;

                                if ( File.Exists( CurrentFigureVectorPath ) )
                                    CurrentVector.LoadVectorFromFile( CurrentFigureVectorPath, Y_frag, X_frag );
                            }
                            else //Создание нового вектора
                            {
                                String CurrentFigureFilePath = TeachingSetsFilePaths.ElementAt( ( int ) ( CurrentTeachingSet * MainPerceptron.Figures_Amount + CurrentFigureIndex ) ); //Путь к текущему файлу изображения

                                CurrentVector = new Vector( CurrentObraz, Y_frag, X_frag );

                                CurrentVector.WriteVectorLog( Path.GetFileNameWithoutExtension( CurrentFigureFilePath ), DefaultTeachingSetsInformationCatalog );
                            }

                            //Обучение

                            bool CurrentTeachingResult = MainPerceptron.TeachPerceptronOnFigure( CurrentVector.complete_vector, MainPerceptron.figures_array[ CurrentFigureIndex ] ); //Попытка обучения

                            if ( CurrentTeachingResult == false )
                                EveryTeachingSuccessful = false;
                        }

                    }

                    ++IterationsAmount;
                }

                return IterationsAmount;
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при обучении на всех обучающих наборах во внутренней функции", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return 0;
            }
        }

        /// <summary>
        /// Имя лога обучения по умолчанию.
        /// </summary>
        private const String DefaultTeachingLogFileName = "TeachingLog.txt";

        private void TeachOnAllTeachingSetsButton_Click( object sender, EventArgs e )
        {
            //Если не задана матрица ответов
            if ( AnswerMatrixCheckBox.Checked == false )
            {
                MessageBox.Show( "Матрица ответов не задана. Задайте матрицу ответов перед обучением.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                return;
            }

            InitializeFragmentations();

            uint IterationsAmount = TeachOnAllTeachingSets( Y_fragmentation, X_fragmentation );

            IterationsAmountTextBox.Text = IterationsAmount.ToString();

            String TeachingLogFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultTeachingLogFileName;

            //Запись в лог
            File.AppendAllText( TeachingLogFilePath, TeachingSetsCount.ToString() + " " + Y_fragmentation.ToString() + " " + X_fragmentation.ToString() + ":" + IterationsAmount.ToString() + Environment.NewLine );

            TeachingDoneCheckBox.Checked = true;
            WeightsMatrixCheckBox.Checked = true;

            MainPerceptron.SaveWeightsMatrix();
        }

        /// <summary>
        /// Имя по умолчанию для лога распознавания
        /// </summary>
        private const String DefaultRecognitionLogFileName = "RecognitionLog.txt";

        private void RecognizeFiguresButton_Click( object sender, EventArgs e )
        {
            OpenFileDialog FiguresToRecognizeFileDialog = new OpenFileDialog();

            try
            {
                FiguresToRecognizeFileDialog.Filter = "Изображения " + DefaultImageExtension + "|*" + DefaultImageExtension; //"Файлы|*.bmp";
                FiguresToRecognizeFileDialog.Multiselect = true;

                RecognitionResultsTextBox.Text = ""; //Стирание предыдущих результатов

                InitializeFragmentations();

                SetMainPerceptronVectorLength( Y_fragmentation, X_fragmentation );

                //Если не получилось считать матрицу весов
                if ( !MainPerceptron.TrySetWeightsMatrix() )
                    return;

                String RecognitionLogFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultRecognitionLogFileName;

                if ( FiguresToRecognizeFileDialog.ShowDialog() == DialogResult.OK )
                {
                    File.AppendAllText( RecognitionLogFilePath, Y_fragmentation.ToString() + " " + X_fragmentation.ToString() + Environment.NewLine ); //Окрытие лога распознавания

                    foreach ( String CurrentFigureFile in FiguresToRecognizeFileDialog.FileNames )
                    {
                        //Работа с образом
                        Obraz CurrentFigureObraz = new Obraz( CurrentFigureFile );

                        String FigureFileName = Path.GetFileNameWithoutExtension( CurrentFigureFile );

                        CurrentFigureObraz.WriteObrazLog( FigureFileName );

                        //Работа с вектором

                        Vector CurrentFigureVector = new Vector( CurrentFigureObraz, Y_fragmentation, X_fragmentation );

                        CurrentFigureVector.WriteVectorLog( FigureFileName );

                        //Получение ответа
                        String NeuralNetworkAnswer = MainPerceptron.GetAnswerFromVector( CurrentFigureVector.complete_vector ); //Ответ сети

                        RecognitionResultsTextBox.Text += ( NeuralNetworkAnswer + " " ); //Запись результата

                        //Запись в лог распознавания
                        File.AppendAllText( RecognitionLogFilePath, NeuralNetworkAnswer + " " );

                    }

                    File.AppendAllText( RecognitionLogFilePath, Environment.NewLine + "///////////////////////" + Environment.NewLine ); //Закрытие лога распознавания
                }

            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при попытке распознавания вне функции распознавания", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            finally
            {
                if ( FiguresToRecognizeFileDialog != null )
                    FiguresToRecognizeFileDialog.Dispose();
            }
        }

        /// <summary>
        /// Имя по умолчанию для лога распознавания при различных разбиениях
        /// </summary>
        private const String DefaultDifferentFragmentationsRecognitionLog = "DifferentFragmentationsRecognitionLog.сsv";

        /// <summary>
        /// Возможные разбиения по горизонтальной составляющей
        /// </summary>
        private uint[] X_fragmentationsArray = { /*4, 5,*/ 6, 10, 20, 50, 100, 150, 300 };

        /// <summary>
        /// Возможные разбиения по вертикальной составляющей
        /// </summary>
        private uint[] Y_fragmentationsArray = { /*4, 5,*/ 6, 10, 20, 50, 100, 150, 300 };

        /// <summary>
        /// Каталог по умолчанию для хранения информации о распознаваниях
        /// </summary>
        private const String DefaultRecognitionInformationCatalog = "\\decisions\\";

        private void DifferentFragmentationsRecognitionButton_Click( object sender, EventArgs e )
        {
            OpenFileDialog FiguresToRecognizeFileDialog = new OpenFileDialog();

            MessageBox.Show( "Все данные по будут помещены в лог " + DefaultDifferentFragmentationsRecognitionLog, "Форма", MessageBoxButtons.OK, MessageBoxIcon.Asterisk );

            try
            {
                //Сброс матрицы весов
                WeightsMatrixCheckBox.Checked = false;

                FiguresToRecognizeFileDialog.Filter = "Изображения " + DefaultImageExtension + "|*" + DefaultImageExtension; //"Файлы|*.bmp";
                FiguresToRecognizeFileDialog.Multiselect = true;

                if ( FiguresToRecognizeFileDialog.ShowDialog() == DialogResult.OK )
                {
                    //Создание и предварительная запись образов для каждого изображения
                    {
                        foreach ( String CurrentFigureFile in FiguresToRecognizeFileDialog.FileNames )
                        {
                            //Работа с образом
                            Obraz CurrentFigureObraz = new Obraz( CurrentFigureFile );

                            String FigureFileName = Path.GetFileNameWithoutExtension( CurrentFigureFile );

                            CurrentFigureObraz.WriteObrazLog( FigureFileName );
                        }
                    }

                    //Инициализация лога 
                    String RecognitionLogFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultDifferentFragmentationsRecognitionLog;

                    File.Delete( RecognitionLogFilePath );


                    foreach ( String FilePath in FiguresToRecognizeFileDialog.FileNames )
                    {
                        String FileName = Path.GetFileName( FilePath );

                        File.AppendAllText( RecognitionLogFilePath, FileName + ";" );
                    }

                    File.AppendAllText( RecognitionLogFilePath, Environment.NewLine + Environment.NewLine );

                    //Распознавание для различных разбиений
                    foreach ( uint Y_fragment in Y_fragmentationsArray )
                    {
                        foreach ( uint X_fragment in X_fragmentationsArray )
                        {
                            SetMainPerceptronVectorLength( Y_fragment, X_fragment );

                            //Обучение при текущих разбиениях
                            TeachOnAllTeachingSets( Y_fragment, X_fragment );

                            File.AppendAllText( RecognitionLogFilePath, Y_fragment.ToString() + " " + X_fragment.ToString() + ";" );

                            foreach ( String CurrentFigureFile in FiguresToRecognizeFileDialog.FileNames )
                            {
                                String ImageName = Path.GetFileNameWithoutExtension( CurrentFigureFile );

                                //Загрузка образов
                                Obraz CurrentFigureObraz = new Obraz();

                                String CurrentFigureObrazFilePath = Directory.GetCurrentDirectory()
                                    + DefaultRecognitionInformationCatalog
                                    + Obraz.GetDefaultObrazName( ImageName );

                                CurrentFigureObraz.LoadObrazFromFile( CurrentFigureObrazFilePath );

                                //Работа с вектором

                                Vector CurrentFigureVector = new Vector( CurrentFigureObraz, Y_fragment, X_fragment );

                                CurrentFigureVector.WriteVectorLog( ImageName );

                                //Получение ответа
                                String NeuralNetworkAnswer = MainPerceptron.GetAnswerFromVector( CurrentFigureVector.complete_vector );

                                //Запись в лог распознавания

                                File.AppendAllText( RecognitionLogFilePath, NeuralNetworkAnswer + ";" );

                            }

                            File.AppendAllText( RecognitionLogFilePath, Environment.NewLine ); //Закрытие строки лога распознавания
                        }
                    }
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при попытке распознавания вне функции распознавания", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }
            finally
            {
                if ( FiguresToRecognizeFileDialog != null )
                    FiguresToRecognizeFileDialog.Dispose();
            }
        }

    }


    class FormPerceptron : Perceptron
    {
        /// <summary>
        /// Конструктор на основании длины вектора
        /// </summary>
        /// <param name="Vect_len"></param>
        public FormPerceptron( uint Vect_len ) : base( Vect_len ) { }

        /// <summary>
        /// Конструктор пустого персептрона формы
        /// </summary>
        public FormPerceptron() : base() { }


        /// <summary>
        /// Попытка получить матрицу весов по умолчанию
        /// </summary>
        /// <returns>Удалось ли получить матрицу</returns>
        public bool TrySetWeightsMatrix()
        {
            try
            {
                String WeightsMatrixFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultWeightsMatrixFileName;

                //Проверка существования файла
                if ( File.Exists( WeightsMatrixFilePath ) )
                {
                    //Попытка считать матрицу
                    try
                    {
                        LoadWeightsMatrix( WeightsMatrixFilePath );
                    }
                    catch ( Exception )
                    {
                        //MainPerceptron.ResetWeightsMatrix();
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch ( Exception )
            {
                MessageBox.Show( "Ошибка при попытке начального считывания матрицы весов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }
        }

        /// <summary>
        /// Попытка получить матрицу ответов по умолчанию
        /// </summary>
        /// <returns>Удалось ли получить матрицу</returns>
        public bool TrySetAnswerMatrix()
        {
            String AnswerMatrixFilePath = Directory.GetCurrentDirectory() + "\\" + DefaultAnswerMatrixFileName;

            try
            {
                //Проверка существования файла
                if ( File.Exists( AnswerMatrixFilePath ) )
                {
                    //Попытка считать матрицу
                    try
                    {
                        LoadAnswerMatrix( AnswerMatrixFilePath );
                    }
                    catch
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                MessageBox.Show( "Ошибка при попытке начального считывания матрицы ответов.", "Форма", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return false;
            }
        }
    }

}
