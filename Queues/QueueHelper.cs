using Queues.Models;
using System;

//using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    public class QueueHelper
    {
        /// <summary>
        /// פעולת ספירת כמות איברים בתור 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static int Count<T>(Queue<T> q)
        {
            int counter = 0;
            //ניצור עותק נוסף של התור
            Queue<T> temp = Copy(q);
            //נרוקן את העותק
            while (!temp.IsEmpty())
            {
                counter++;
                temp.Remove();
            }
            //נחזיר את הכמות
            return counter;
        }
        /// <summary>
        /// פעולה הסופרת כמות איברים בתור ללא שימוש בפעולת עזר
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>

        public static int Count2<T>(Queue<T> q)
        {
            int counter = 0;
            Queue<T> temp = new Queue<T>();
            //נעתיק את הערכים לתור חדש
            while (!q.IsEmpty())
            {
                temp.Insert(q.Remove());
                counter++;
            }
            //נחזיר את הערכים חזרה לתור המקורי
            while (!temp.IsEmpty())
            {
                q.Insert(temp.Remove());
            }
            //נחזיר את הכמות
            return counter;
        }
        /// <summary>
        /// פעולה היוצרת עותק של התור
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Queue<T> Copy<T>(Queue<T> original)
        {
            Queue<T> copy = new Queue<T>();
            Queue<T> temp = new Queue<T>();
            while (!original.IsEmpty())
            {
                temp.Insert(original.Remove());

            }
            while (!temp.IsEmpty())
            {
                copy.Insert(temp.Head());
                original.Insert(temp.Remove());
            }
            return copy;

        }

        //אם לא כתוב אחרת אסור להרוס את התור

        //פעולה שמחזירה טרו או פולס האם התור ממויין בסדר עולה
        public static bool IsAsc(Queue<int> q)
        {
            int min = int.MaxValue;
            int temphead = int.MaxValue;
            Queue<int> temp = Copy(q);
            while (!temp.IsEmpty())//כל עוד התור לא ריק
            {
                min = temp.Remove();
                if (!temp.IsEmpty())
                { 
                    temphead = temp.Head();
                }
                if (temphead == int.MaxValue)
                {
                    return true;
                }
                if (min > temphead)
                    return false;
            }
            return true;
        }


        //פעולה שמקבלת תור של מספרים שלמים ומחזירה אתצ המינימלי בלי להוציא אותו
        public static int MinVal(Queue<int> q)
        {
            if(q.IsEmpty())
                return int.MinValue;
            Queue<int> temp = Copy(q);
            int min = temp.Remove();
            while (!temp.IsEmpty())
            {
                if (min > temp.Head())
                    min = temp.Remove();
                else temp.Remove();
            }
            return min;
        }

        //פעולה שמקבלת תור שלמים ומוציאה את המינימלי

        public static void Removemin(Queue<int> q)
        {
            int min = MinVal(q);
            Queue<int> temp = new Queue<int>();
            while (!q.IsEmpty())
            {
                if (q.Head() != min)
                    temp.Insert(q.Remove());
                else
                    q.Remove();
            }
            while(!temp.IsEmpty())
            {
                q.Insert(temp.Remove());
            }
        }

        public static Queue<int> NewAsc(Queue<int> q)
        {
            Queue<int> newQ = new Queue<int>();
            Queue<int> origin = Copy(q);

            while (!origin.IsEmpty())
            {
                newQ.Insert(MinVal(origin));
                Removemin(origin);
            }
            return newQ;
        }

        public static void EnterMidle<T>(Queue<T> q, T val)
        {
            int length = Count(q);//חישוב כמות האיברים
            Queue<T> temp = new Queue<T>();//תור עזר
            for (int i = length/2; i < length; i++)//רצים עד לאמצע התור ומעבירים חצי ממנו לתור העזר
            {
                temp.Insert(q.Remove());
            }
            temp.Insert(val);//הכנסה לאמצע בתור העזר
            while (!q.IsEmpty())//מכניס את חצי השני של התור המקורי לסוף תור העזר
            {
                temp.Insert(q.Remove());
            }
            while (!temp.IsEmpty())//מחזיר את כל התור לתור המקורי
            {
                q.Insert(temp.Remove());
            }
        }

        public static void NiceMerge<T>(Queue<T> Q1st, Queue<T> Q2st)
        {
            Queue<T> temp = new Queue<T>();
            while(!Q1st.IsEmpty())
            {
                temp.Insert(Q1st.Remove());
            }
            if (!temp.IsEmpty())
            {
                while (!Q2st.IsEmpty()&&!temp.IsEmpty())
                {
                    Q1st.Insert(temp.Remove());
                    Q1st.Insert(Q2st.Remove());
                }
            }
            else
            {
                while (!Q2st.IsEmpty())
                {
                    Q1st.Insert(Q2st.Remove());
                }
            }
            while (!temp.IsEmpty()|| !Q2st.IsEmpty())
            {
                if (!temp.IsEmpty())
                {
                    Q1st.Insert(temp.Remove());
                }
                if(!Q2st.IsEmpty())
                {
                    Q1st.Insert(Q2st.Remove());
                }
            }
        }


    }
}
