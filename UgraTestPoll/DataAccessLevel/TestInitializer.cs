using System.Collections.Generic;
using UgraTestPoll.Models;

namespace UgraTestPoll.DataAccessLevel
{
    /// <summary>
    /// Crate the sample data for database
    /// </summary>
    public class TestInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PollContext>
    {
        protected override void Seed(PollContext context)
        {
            var tests = new List<Test>
            {
            new Test{Name="Java Test", Active=true},
            new Test{Name="C# Test", Active=true},
            new Test{Name="Python test", Active=true},
            };
            tests.ForEach(s => context.Tests.Add(s));
            context.SaveChanges();

            var questions = new List<Question>
            {
            /* Java */
            new RadioQuestion{QuestionText="Как можно уничтожить объект в Java?", Number=1, Active=true, TestId=tests[0].ID},
            new CheckboxQuestion{QuestionText="Перечислите все валидные сигнатуры конструкторов класса Clazz:", Number=2, Active=true, TestId=tests[0].ID},
            new CheckboxQuestion{QuestionText="Какие интерфейсы предоставляют возможность хранить объекты в виде пары \"ключ-значение\"?", Number=3, Active=true, TestId=tests[0].ID},
            new CheckboxQuestion{QuestionText="Перечислите все методы, которые есть у класса Object.", Number=4, Active=true, TestId=tests[0].ID},
            new InputQuestion{QuestionText="Какой класс является базовым для всех классов Java?", Number=5, Active=true, TestId=tests[0].ID},
            /* C# */
            new RadioQuestion{QuestionText="Допустимо ли множественное наследование (классов) в C#?", Number=1, Active=true, TestId=tests[1].ID},
            new CheckboxQuestion{QuestionText="Каким образом можно перехватить добавление и удаление делегатов из события?", Number=2, Active=true, TestId=tests[1].ID},
            new CheckboxQuestion{QuestionText="Выберите средства, которые предоставляет С# для условной компиляции", Number=3, Active=true, TestId=tests[1].ID},
            new InputQuestion{QuestionText="Как называется технология, благодаря которой возможно взаимодействие управляемого кода (managed code) с Win32 API функциями и COM-объектами?", Number=4, Active=true, TestId=tests[1].ID},
            /* Python */
            new RadioQuestion{QuestionText="Что из перечисленного НЕ является ключевым словом python?", Number=1, Active=true, TestId=tests[2].ID},
            new CheckboxQuestion{QuestionText="Python является языком с _____ типизацией? (выберите все подходящие варианты)", Number=2, Active=true, TestId=tests[2].ID},
            new InputQuestion{QuestionText="Блок функции в Python начинается с ключевого слова:", Number=3, Active=true, TestId=tests[2].ID},
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>
            {
            /* Java */
            new RadionAnswer{AnswerText="присвоить null всем ссылкам на объект", Active=true, Correct=false, QuestionID=questions[0].ID},
            new RadionAnswer{AnswerText="вызвать Runtime.getRuntime().gc()", Active=true, Correct=false, QuestionID=questions[0].ID},
            new RadionAnswer{AnswerText="вызвать метод finalize() у объекта", Active=true, Correct=false, QuestionID=questions[0].ID},
            new RadionAnswer{AnswerText="этого нельзя сделать вручную", Active=true, Correct=true, QuestionID=questions[0].ID},
            new RadionAnswer{AnswerText="вызвать деструктор у объекта", Active=true, Correct=false, QuestionID=questions[0].ID},
            new CheckBoxAnswer{AnswerText="Clazz(String name)", Active=true, Correct=true, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="Clazz Clazz(String name)", Active=true, Correct=false, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="int Clazz(String name)", Active=true, Correct=false, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="void Clazz(String name)", Active=true, Correct=false, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="Clazz(name)", Active=true, Correct=false, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="Clazz()", Active=true, Correct=true, QuestionID=questions[1].ID},
            new CheckBoxAnswer{AnswerText="java.util.Map", Active=true, Correct=true, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="java.util.List", Active=true, Correct=false, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="java.util.Set", Active=true, Correct=false, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="java.util.SortedSet", Active=true, Correct=false, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="java.util.SortedMap", Active=true, Correct=true, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="java.util.Collection", Active=true, Correct=false, QuestionID=questions[2].ID},
            new CheckBoxAnswer{AnswerText="equals", Active=true, Correct=true, QuestionID=questions[3].ID},
            new CheckBoxAnswer{AnswerText="toString", Active=true, Correct=true, QuestionID=questions[3].ID},
            new CheckBoxAnswer{AnswerText="hashCode", Active=true, Correct=true, QuestionID=questions[3].ID},
            new CheckBoxAnswer{AnswerText="clone", Active=true, Correct=true, QuestionID=questions[3].ID},
            new InputAnswer{AnswerText="Object", Active=true, Correct=true, QuestionID=questions[4].ID},
            /* C# */
            new RadionAnswer{AnswerText="Да", Active=true, Correct=false, QuestionID=questions[5].ID},
            new RadionAnswer{AnswerText="Нет, но возможна реализация нескольких интерфейсов", Active=true, Correct=true, QuestionID=questions[5].ID},
            new CheckBoxAnswer{AnswerText="Такая возможность не предусмотрена", Active=true, Correct=false, QuestionID=questions[6].ID},
            new CheckBoxAnswer{AnswerText="Для этого существуют специальные ключевые слова add и remove", Active=true, Correct=true, QuestionID=questions[6].ID},
            new CheckBoxAnswer{AnswerText="Использовать ключевые слова get и set", Active=true, Correct=false, QuestionID=questions[6].ID},
            new CheckBoxAnswer{AnswerText="Переопределить операторы + и - для делегата", Active=true, Correct=false, QuestionID=questions[6].ID},
            new CheckBoxAnswer{AnswerText="Такая возможность не предусмотрена", Active=true, Correct=false, QuestionID=questions[6].ID},
            new CheckBoxAnswer{AnswerText="Директива #if", Active=true, Correct=true, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #endif", Active=true, Correct=true, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #else", Active=true, Correct=true, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #typedef", Active=true, Correct=false, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #define", Active=true, Correct=true, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #Conditional", Active=true, Correct=true, QuestionID=questions[7].ID},
            new CheckBoxAnswer{AnswerText="Директива #elseif", Active=true, Correct=false, QuestionID=questions[7].ID},
            new InputAnswer{AnswerText="Interop", Active=true, Correct=true, QuestionID=questions[8].ID},
            /* Python */
            new RadionAnswer{AnswerText="lambda", Active=true, Correct=false, QuestionID=questions[9].ID},
            new RadionAnswer{AnswerText="yield", Active=true, Correct=false, QuestionID=questions[9].ID},
            new RadionAnswer{AnswerText="closure", Active=true, Correct=true, QuestionID=questions[9].ID},
            new CheckBoxAnswer{AnswerText="Сильной", Active=true, Correct=true, QuestionID=questions[10].ID},
            new CheckBoxAnswer{AnswerText="Слабой", Active=true, Correct=false, QuestionID=questions[10].ID},
            new CheckBoxAnswer{AnswerText="Статической", Active=true, Correct=false, QuestionID=questions[10].ID},
            new CheckBoxAnswer{AnswerText="Динамической", Active=true, Correct=true, QuestionID=questions[10].ID},
            new CheckBoxAnswer{AnswerText="Явной", Active=true, Correct=false, QuestionID=questions[10].ID},
            new CheckBoxAnswer{AnswerText="Неявной", Active=true, Correct=true, QuestionID=questions[10].ID},
            new InputAnswer{AnswerText="def", Active=true, Correct=true, QuestionID=questions[11].ID},
            };           
            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();
        }
    }
}