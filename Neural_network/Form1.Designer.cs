namespace Neural_network
{
    partial class MainInterfaceForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecognizeFiguresButton = new System.Windows.Forms.Button();
            this.WeightsMatrixTitleLabel = new System.Windows.Forms.Label();
            this.WeightsMatrixCheckBox = new System.Windows.Forms.CheckBox();
            this.ResetWeightsMatrixButton = new System.Windows.Forms.Button();
            this.AnswerMatrixTitleLabel = new System.Windows.Forms.Label();
            this.AnswerMatrixCheckBox = new System.Windows.Forms.CheckBox();
            this.ResetAnswerMatrixButton = new System.Windows.Forms.Button();
            this.TeachingSetsTitleLabel = new System.Windows.Forms.Label();
            this.TeachingSetsCountTextBox = new System.Windows.Forms.TextBox();
            this.AddTeachingSetsButton = new System.Windows.Forms.Button();
            this.FragmentationTitleLabel = new System.Windows.Forms.Label();
            this.X_FragmentationTextBox = new System.Windows.Forms.TextBox();
            this.Y_FragmentationTextBox = new System.Windows.Forms.TextBox();
            this.X_FragmentationLabel = new System.Windows.Forms.Label();
            this.Y_FragmentationLabel = new System.Windows.Forms.Label();
            this.NeuronsAmountTitleLabel = new System.Windows.Forms.Label();
            this.Neurons_AmountTextBox = new System.Windows.Forms.TextBox();
            this.TeachingSetsCountLabel = new System.Windows.Forms.Label();
            this.TeachingTitleLabel = new System.Windows.Forms.Label();
            this.TeachOnAllTeachingSetsButton = new System.Windows.Forms.Button();
            this.TeachingDoneCheckBox = new System.Windows.Forms.CheckBox();
            this.RecognitionResultsTextBox = new System.Windows.Forms.TextBox();
            this.DifferentFragmentationsRecognitionButton = new System.Windows.Forms.Button();
            this.DeleteTeachingSetsButton = new System.Windows.Forms.Button();
            this.IterationsAmountLabel = new System.Windows.Forms.Label();
            this.IterationsAmountTextBox = new System.Windows.Forms.TextBox();
            this.RecognitionTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RecognizeFiguresButton
            // 
            this.RecognizeFiguresButton.Location = new System.Drawing.Point(7, 201);
            this.RecognizeFiguresButton.Name = "RecognizeFiguresButton";
            this.RecognizeFiguresButton.Size = new System.Drawing.Size(141, 23);
            this.RecognizeFiguresButton.TabIndex = 0;
            this.RecognizeFiguresButton.Text = "Распознавание фигур";
            this.RecognizeFiguresButton.UseVisualStyleBackColor = true;
            this.RecognizeFiguresButton.Click += new System.EventHandler(this.RecognizeFiguresButton_Click);
            // 
            // WeightsMatrixTitleLabel
            // 
            this.WeightsMatrixTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.WeightsMatrixTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WeightsMatrixTitleLabel.Location = new System.Drawing.Point(417, 167);
            this.WeightsMatrixTitleLabel.Name = "WeightsMatrixTitleLabel";
            this.WeightsMatrixTitleLabel.Size = new System.Drawing.Size(197, 19);
            this.WeightsMatrixTitleLabel.TabIndex = 1;
            this.WeightsMatrixTitleLabel.Text = "Матрица весов";
            this.WeightsMatrixTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WeightsMatrixCheckBox
            // 
            this.WeightsMatrixCheckBox.AutoCheck = false;
            this.WeightsMatrixCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WeightsMatrixCheckBox.Location = new System.Drawing.Point(417, 189);
            this.WeightsMatrixCheckBox.Name = "WeightsMatrixCheckBox";
            this.WeightsMatrixCheckBox.Size = new System.Drawing.Size(197, 19);
            this.WeightsMatrixCheckBox.TabIndex = 2;
            this.WeightsMatrixCheckBox.Text = "Матрица весов загружена";
            this.WeightsMatrixCheckBox.UseVisualStyleBackColor = true;
            // 
            // ResetWeightsMatrixButton
            // 
            this.ResetWeightsMatrixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetWeightsMatrixButton.Location = new System.Drawing.Point(417, 214);
            this.ResetWeightsMatrixButton.Name = "ResetWeightsMatrixButton";
            this.ResetWeightsMatrixButton.Size = new System.Drawing.Size(197, 23);
            this.ResetWeightsMatrixButton.TabIndex = 3;
            this.ResetWeightsMatrixButton.Text = "Обнуление матрицы весов";
            this.ResetWeightsMatrixButton.UseVisualStyleBackColor = true;
            this.ResetWeightsMatrixButton.Click += new System.EventHandler(this.ResetWeightsMatrixButton_Click);
            // 
            // AnswerMatrixTitleLabel
            // 
            this.AnswerMatrixTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnswerMatrixTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerMatrixTitleLabel.Location = new System.Drawing.Point(417, 257);
            this.AnswerMatrixTitleLabel.Name = "AnswerMatrixTitleLabel";
            this.AnswerMatrixTitleLabel.Size = new System.Drawing.Size(197, 18);
            this.AnswerMatrixTitleLabel.TabIndex = 4;
            this.AnswerMatrixTitleLabel.Text = "Матрица ответов";
            this.AnswerMatrixTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AnswerMatrixCheckBox
            // 
            this.AnswerMatrixCheckBox.AutoCheck = false;
            this.AnswerMatrixCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerMatrixCheckBox.Location = new System.Drawing.Point(417, 279);
            this.AnswerMatrixCheckBox.Name = "AnswerMatrixCheckBox";
            this.AnswerMatrixCheckBox.Size = new System.Drawing.Size(197, 19);
            this.AnswerMatrixCheckBox.TabIndex = 5;
            this.AnswerMatrixCheckBox.Text = "Матрица ответов задана";
            this.AnswerMatrixCheckBox.UseVisualStyleBackColor = true;
            // 
            // ResetAnswerMatrixButton
            // 
            this.ResetAnswerMatrixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResetAnswerMatrixButton.Location = new System.Drawing.Point(417, 304);
            this.ResetAnswerMatrixButton.Name = "ResetAnswerMatrixButton";
            this.ResetAnswerMatrixButton.Size = new System.Drawing.Size(197, 43);
            this.ResetAnswerMatrixButton.TabIndex = 6;
            this.ResetAnswerMatrixButton.Text = "Реинициализация матрицы ответов";
            this.ResetAnswerMatrixButton.UseVisualStyleBackColor = true;
            this.ResetAnswerMatrixButton.Click += new System.EventHandler(this.ResetAnswerMatrixButton_Click);
            // 
            // TeachingSetsTitleLabel
            // 
            this.TeachingSetsTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TeachingSetsTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeachingSetsTitleLabel.Location = new System.Drawing.Point(417, 365);
            this.TeachingSetsTitleLabel.Name = "TeachingSetsTitleLabel";
            this.TeachingSetsTitleLabel.Size = new System.Drawing.Size(197, 19);
            this.TeachingSetsTitleLabel.TabIndex = 7;
            this.TeachingSetsTitleLabel.Text = "Обучающие наборы";
            this.TeachingSetsTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TeachingSetsCountTextBox
            // 
            this.TeachingSetsCountTextBox.Location = new System.Drawing.Point(420, 431);
            this.TeachingSetsCountTextBox.Name = "TeachingSetsCountTextBox";
            this.TeachingSetsCountTextBox.ReadOnly = true;
            this.TeachingSetsCountTextBox.Size = new System.Drawing.Size(194, 20);
            this.TeachingSetsCountTextBox.TabIndex = 8;
            this.TeachingSetsCountTextBox.Text = "0";
            // 
            // AddTeachingSetsButton
            // 
            this.AddTeachingSetsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddTeachingSetsButton.Location = new System.Drawing.Point(417, 457);
            this.AddTeachingSetsButton.Name = "AddTeachingSetsButton";
            this.AddTeachingSetsButton.Size = new System.Drawing.Size(197, 23);
            this.AddTeachingSetsButton.TabIndex = 9;
            this.AddTeachingSetsButton.Text = "Добавить обучающие наборы";
            this.AddTeachingSetsButton.UseVisualStyleBackColor = true;
            this.AddTeachingSetsButton.Click += new System.EventHandler(this.AddTeachingSetsButton_Click);
            // 
            // FragmentationTitleLabel
            // 
            this.FragmentationTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FragmentationTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FragmentationTitleLabel.Location = new System.Drawing.Point(417, 78);
            this.FragmentationTitleLabel.Name = "FragmentationTitleLabel";
            this.FragmentationTitleLabel.Size = new System.Drawing.Size(197, 19);
            this.FragmentationTitleLabel.TabIndex = 11;
            this.FragmentationTitleLabel.Text = "Количество разбиений";
            this.FragmentationTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // X_FragmentationTextBox
            // 
            this.X_FragmentationTextBox.Location = new System.Drawing.Point(465, 126);
            this.X_FragmentationTextBox.Name = "X_FragmentationTextBox";
            this.X_FragmentationTextBox.Size = new System.Drawing.Size(148, 20);
            this.X_FragmentationTextBox.TabIndex = 12;
            this.X_FragmentationTextBox.Text = "6";
            // 
            // Y_FragmentationTextBox
            // 
            this.Y_FragmentationTextBox.Location = new System.Drawing.Point(466, 100);
            this.Y_FragmentationTextBox.Name = "Y_FragmentationTextBox";
            this.Y_FragmentationTextBox.Size = new System.Drawing.Size(148, 20);
            this.Y_FragmentationTextBox.TabIndex = 13;
            this.Y_FragmentationTextBox.Text = "6";
            // 
            // X_FragmentationLabel
            // 
            this.X_FragmentationLabel.AutoSize = true;
            this.X_FragmentationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.X_FragmentationLabel.Location = new System.Drawing.Point(416, 126);
            this.X_FragmentationLabel.Name = "X_FragmentationLabel";
            this.X_FragmentationLabel.Size = new System.Drawing.Size(43, 17);
            this.X_FragmentationLabel.TabIndex = 14;
            this.X_FragmentationLabel.Text = "По Х:";
            // 
            // Y_FragmentationLabel
            // 
            this.Y_FragmentationLabel.AutoSize = true;
            this.Y_FragmentationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Y_FragmentationLabel.Location = new System.Drawing.Point(417, 100);
            this.Y_FragmentationLabel.Name = "Y_FragmentationLabel";
            this.Y_FragmentationLabel.Size = new System.Drawing.Size(43, 17);
            this.Y_FragmentationLabel.TabIndex = 15;
            this.Y_FragmentationLabel.Text = "По Y:";
            // 
            // NeuronsAmountTitleLabel
            // 
            this.NeuronsAmountTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NeuronsAmountTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NeuronsAmountTitleLabel.Location = new System.Drawing.Point(417, 9);
            this.NeuronsAmountTitleLabel.Name = "NeuronsAmountTitleLabel";
            this.NeuronsAmountTitleLabel.Size = new System.Drawing.Size(197, 19);
            this.NeuronsAmountTitleLabel.TabIndex = 16;
            this.NeuronsAmountTitleLabel.Text = "Количество нейронов";
            this.NeuronsAmountTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Neurons_AmountTextBox
            // 
            this.Neurons_AmountTextBox.Location = new System.Drawing.Point(417, 30);
            this.Neurons_AmountTextBox.Name = "Neurons_AmountTextBox";
            this.Neurons_AmountTextBox.ReadOnly = true;
            this.Neurons_AmountTextBox.Size = new System.Drawing.Size(197, 20);
            this.Neurons_AmountTextBox.TabIndex = 17;
            this.Neurons_AmountTextBox.Text = "4";
            // 
            // TeachingSetsCountLabel
            // 
            this.TeachingSetsCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeachingSetsCountLabel.Location = new System.Drawing.Point(417, 393);
            this.TeachingSetsCountLabel.Name = "TeachingSetsCountLabel";
            this.TeachingSetsCountLabel.Size = new System.Drawing.Size(197, 35);
            this.TeachingSetsCountLabel.TabIndex = 18;
            this.TeachingSetsCountLabel.Text = "Количество обучающих наборов:";
            this.TeachingSetsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TeachingTitleLabel
            // 
            this.TeachingTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TeachingTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeachingTitleLabel.Location = new System.Drawing.Point(9, 6);
            this.TeachingTitleLabel.Name = "TeachingTitleLabel";
            this.TeachingTitleLabel.Size = new System.Drawing.Size(103, 19);
            this.TeachingTitleLabel.TabIndex = 19;
            this.TeachingTitleLabel.Text = "Обучение";
            // 
            // TeachOnAllTeachingSetsButton
            // 
            this.TeachOnAllTeachingSetsButton.Location = new System.Drawing.Point(7, 115);
            this.TeachOnAllTeachingSetsButton.Margin = new System.Windows.Forms.Padding(2);
            this.TeachOnAllTeachingSetsButton.Name = "TeachOnAllTeachingSetsButton";
            this.TeachOnAllTeachingSetsButton.Size = new System.Drawing.Size(143, 39);
            this.TeachOnAllTeachingSetsButton.TabIndex = 20;
            this.TeachOnAllTeachingSetsButton.Text = "Обучение на всех обучающих наборах";
            this.TeachOnAllTeachingSetsButton.UseVisualStyleBackColor = true;
            this.TeachOnAllTeachingSetsButton.Click += new System.EventHandler(this.TeachOnAllTeachingSetsButton_Click);
            // 
            // TeachingDoneCheckBox
            // 
            this.TeachingDoneCheckBox.AutoCheck = false;
            this.TeachingDoneCheckBox.AutoSize = true;
            this.TeachingDoneCheckBox.Location = new System.Drawing.Point(9, 31);
            this.TeachingDoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.TeachingDoneCheckBox.Name = "TeachingDoneCheckBox";
            this.TeachingDoneCheckBox.Size = new System.Drawing.Size(143, 17);
            this.TeachingDoneCheckBox.TabIndex = 21;
            this.TeachingDoneCheckBox.Text = "Обучение произведено";
            this.TeachingDoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // RecognitionResultsTextBox
            // 
            this.RecognitionResultsTextBox.Location = new System.Drawing.Point(7, 230);
            this.RecognitionResultsTextBox.Multiline = true;
            this.RecognitionResultsTextBox.Name = "RecognitionResultsTextBox";
            this.RecognitionResultsTextBox.ReadOnly = true;
            this.RecognitionResultsTextBox.Size = new System.Drawing.Size(93, 67);
            this.RecognitionResultsTextBox.TabIndex = 22;
            this.RecognitionResultsTextBox.Text = "Результаты распознавания";
            // 
            // DifferentFragmentationsRecognitionButton
            // 
            this.DifferentFragmentationsRecognitionButton.Location = new System.Drawing.Point(7, 305);
            this.DifferentFragmentationsRecognitionButton.Name = "DifferentFragmentationsRecognitionButton";
            this.DifferentFragmentationsRecognitionButton.Size = new System.Drawing.Size(141, 41);
            this.DifferentFragmentationsRecognitionButton.TabIndex = 23;
            this.DifferentFragmentationsRecognitionButton.Text = "Распознавание при разных разбиениях";
            this.DifferentFragmentationsRecognitionButton.UseVisualStyleBackColor = true;
            this.DifferentFragmentationsRecognitionButton.Click += new System.EventHandler(this.DifferentFragmentationsRecognitionButton_Click);
            // 
            // DeleteTeachingSetsButton
            // 
            this.DeleteTeachingSetsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteTeachingSetsButton.Location = new System.Drawing.Point(417, 487);
            this.DeleteTeachingSetsButton.Name = "DeleteTeachingSetsButton";
            this.DeleteTeachingSetsButton.Size = new System.Drawing.Size(196, 43);
            this.DeleteTeachingSetsButton.TabIndex = 24;
            this.DeleteTeachingSetsButton.Text = "Удалить все обучающие наборы";
            this.DeleteTeachingSetsButton.UseVisualStyleBackColor = true;
            this.DeleteTeachingSetsButton.Click += new System.EventHandler(this.DeleteTeachingSetsButton_Click);
            // 
            // IterationsAmountLabel
            // 
            this.IterationsAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IterationsAmountLabel.Location = new System.Drawing.Point(6, 50);
            this.IterationsAmountLabel.Name = "IterationsAmountLabel";
            this.IterationsAmountLabel.Size = new System.Drawing.Size(146, 37);
            this.IterationsAmountLabel.TabIndex = 25;
            this.IterationsAmountLabel.Text = "Количество итераций обучения:";
            // 
            // IterationsAmountTextBox
            // 
            this.IterationsAmountTextBox.Location = new System.Drawing.Point(9, 90);
            this.IterationsAmountTextBox.Name = "IterationsAmountTextBox";
            this.IterationsAmountTextBox.Size = new System.Drawing.Size(143, 20);
            this.IterationsAmountTextBox.TabIndex = 26;
            // 
            // RecognitionTitleLabel
            // 
            this.RecognitionTitleLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RecognitionTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecognitionTitleLabel.Location = new System.Drawing.Point(9, 179);
            this.RecognitionTitleLabel.Name = "RecognitionTitleLabel";
            this.RecognitionTitleLabel.Size = new System.Drawing.Size(127, 19);
            this.RecognitionTitleLabel.TabIndex = 27;
            this.RecognitionTitleLabel.Text = "Распознавание";
            // 
            // MainInterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 542);
            this.Controls.Add(this.RecognitionTitleLabel);
            this.Controls.Add(this.IterationsAmountTextBox);
            this.Controls.Add(this.IterationsAmountLabel);
            this.Controls.Add(this.DeleteTeachingSetsButton);
            this.Controls.Add(this.DifferentFragmentationsRecognitionButton);
            this.Controls.Add(this.RecognitionResultsTextBox);
            this.Controls.Add(this.TeachingDoneCheckBox);
            this.Controls.Add(this.TeachOnAllTeachingSetsButton);
            this.Controls.Add(this.TeachingTitleLabel);
            this.Controls.Add(this.TeachingSetsCountLabel);
            this.Controls.Add(this.Neurons_AmountTextBox);
            this.Controls.Add(this.NeuronsAmountTitleLabel);
            this.Controls.Add(this.Y_FragmentationLabel);
            this.Controls.Add(this.X_FragmentationLabel);
            this.Controls.Add(this.Y_FragmentationTextBox);
            this.Controls.Add(this.X_FragmentationTextBox);
            this.Controls.Add(this.FragmentationTitleLabel);
            this.Controls.Add(this.AddTeachingSetsButton);
            this.Controls.Add(this.TeachingSetsCountTextBox);
            this.Controls.Add(this.TeachingSetsTitleLabel);
            this.Controls.Add(this.ResetAnswerMatrixButton);
            this.Controls.Add(this.AnswerMatrixCheckBox);
            this.Controls.Add(this.AnswerMatrixTitleLabel);
            this.Controls.Add(this.ResetWeightsMatrixButton);
            this.Controls.Add(this.WeightsMatrixCheckBox);
            this.Controls.Add(this.WeightsMatrixTitleLabel);
            this.Controls.Add(this.RecognizeFiguresButton);
            this.Name = "MainInterfaceForm";
            this.Text = "Нейронная сеть";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RecognizeFiguresButton;
        private System.Windows.Forms.Label WeightsMatrixTitleLabel;
        private System.Windows.Forms.CheckBox WeightsMatrixCheckBox;
        private System.Windows.Forms.Button ResetWeightsMatrixButton;
        private System.Windows.Forms.Label AnswerMatrixTitleLabel;
        private System.Windows.Forms.CheckBox AnswerMatrixCheckBox;
        private System.Windows.Forms.Button ResetAnswerMatrixButton;
        private System.Windows.Forms.Label TeachingSetsTitleLabel;
        private System.Windows.Forms.TextBox TeachingSetsCountTextBox;
        private System.Windows.Forms.Button AddTeachingSetsButton;
        private System.Windows.Forms.Label FragmentationTitleLabel;
        private System.Windows.Forms.TextBox X_FragmentationTextBox;
        private System.Windows.Forms.TextBox Y_FragmentationTextBox;
        private System.Windows.Forms.Label X_FragmentationLabel;
        private System.Windows.Forms.Label Y_FragmentationLabel;
        private System.Windows.Forms.Label NeuronsAmountTitleLabel;
        private System.Windows.Forms.TextBox Neurons_AmountTextBox;
        private System.Windows.Forms.Label TeachingSetsCountLabel;
        private System.Windows.Forms.Label TeachingTitleLabel;
        private System.Windows.Forms.Button TeachOnAllTeachingSetsButton;
        private System.Windows.Forms.CheckBox TeachingDoneCheckBox;
        private System.Windows.Forms.TextBox RecognitionResultsTextBox;
        private System.Windows.Forms.Button DifferentFragmentationsRecognitionButton;
        private System.Windows.Forms.Button DeleteTeachingSetsButton;
        private System.Windows.Forms.Label IterationsAmountLabel;
        private System.Windows.Forms.TextBox IterationsAmountTextBox;
        private System.Windows.Forms.Label RecognitionTitleLabel;
    }
}

