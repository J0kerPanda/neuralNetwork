Simple perceptron-based neural network for determining numbers on images.

![Alt text](https://cloud.githubusercontent.com/assets/16634903/22876841/8f82f8d2-f1e3-11e6-987d-c57e7e2c3b78.png "Optional title")

Instruction (Russian):

Данная нейронная сеть обучается по следующим принципам:
1. Изображение преобразуется в чёрно-белое и анализируются прямоугольники, на которое разбивается изображение, по процентному содержанию
белого и чёрного цветов.
2. Проводятся прямые ( горизонтальные и вертикальные ), частота которых зависит от числа разбиений на прямоугольники, после чего 
анализируется, сколько раз прямая пересекает границу цифры.
3. На основе пунктов 1 и 2 задаются коэффициенты матрицы весов, которые корректируются с каждым изображением.

Пункты 1, 2, 3 повторяются до тех пор, пока все изображения из обучающих наборов не будут распознаваться корректно.
Корректность определяется путём сопоставления вектора-ответа ( в данном случае - двоичная запись цифры в 4 битах ) на вектор, полученный
путем умножения вектора компонент из пунктов 1,2 на матрицу весов.

Важно: в каждом обучающем наборе строго 10 цифр ( от 0 до 9 соответственно ), каждая хранится в изображении формата .bmp с наименованием
номерНабора_цифра.bmp. Это необходимо учитывать при добавлении новых обучающих наборов.

После обучения произвольные изображения с цифрами можно попытаться распознать ( как для проведенного обучения на конкретном разбиении, так
и на многих, предварительно заданных разбиениях )
