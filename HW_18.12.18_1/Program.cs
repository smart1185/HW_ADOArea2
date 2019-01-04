using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_18._12._18_1
{
    class Program
    {
        static public Model1 db;
        static void Main(string[] args)
        {
            //SqlToDataMethod();
            //SqlToDataMethod1();
            //SqlToDataMethod2();
            //SqlToDataMethod3();
            //SqlToDataMethod4();
            //SqlToDataMethod5();
            SqlToDataMethod6();
            //SqlToDataMethod7();
            //SqlToDataMethod8();
            //SqlToDataMethod9();
            //SqlToDataMethod10();
            //SqlToDataMethod11();
            //SqlToDataMethod12();
        }

        static void SqlToDataMethod()
        {
            db = new Model1();

            var query = from q in db.Area
                        let wp = q.WorkingPeople
                        where wp > 2
                        select q;

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "\t" + item.IP + "\t" + item.WorkingPeople);
            }
        }

        static void SqlToDataMethod1()
        {
            db = new Model1();

            var query = db.Area
                .Where(w => w.AssemblyArea == true)
                .Select(s => s);

            foreach (var item in query)
            {
                Console.WriteLine(item.AreaId + "\t" + item.Name + "\t" + item.AssemblyArea);
            }
        }

        static void SqlToDataMethod2()
        {
            db = new Model1();

            var query = db.Area
                .Take(10);

            foreach (var item in query)
            {
                Console.WriteLine(item.AreaId + "\t" + item.Name);
            }
        }
        static void SqlToDataMethod3()
        {
            db = new Model1();

            var query = db.Area
                .OrderBy(o=>o.AreaId)
                .Skip(4)
                .Take(4);

            foreach (var item in query)
            {
                Console.WriteLine(item.AreaId + "\t" + item.Name);
            }
        }

        static void SqlToDataMethod4()
        {
            db = new Model1();

            var query = db.Area.ToList();                            
            var query1 = query.TakeWhile(t=> t.OrderExecution != 0).ToList();

            foreach (var item in query1)
            {
                Console.WriteLine("AreaId = {0}, Name = {1}, OrderExecution = {2}", item.AreaId, item.Name, item.OrderExecution);
            }
        }

        static void SqlToDataMethod5()
        {
            db = new Model1();

            var query = db.Area.ToList();
            var query1 = query.TakeWhile(t => t.OrderExecution == 0);

            foreach (var item in query1)
            {
                Console.WriteLine(item.AreaId + "\t" + item.Name + "\t" + item.OrderExecution);
            }
        }

        static void SqlToDataMethod6()
        {
            db = new Model1();


            var list = db.Area.ToList();

            var distinctKeys = list.Select(e => new { e.AreaId, e.Name, e.IP })
                            .Distinct();

            foreach (var item in distinctKeys)
            {
                Console.WriteLine(item.AreaId + "\t" + item.IP);
            }



        }

        static void SqlToDataMethod7()
        {
            db = new Model1();

            var query = db.Timer.Where(w=> w.AreaId >=22 && w.AreaId <=28).Select(s=>s).ToList();
            

            foreach (var item in query)
            {
                Console.WriteLine("TimerId = {0}, User Id ={1}, Area ID = {2}, Date Start = {3}", item.TimerId, item.UserId, item.AreaId, item.DateStart);
            }
        }

        static void SqlToDataMethod8()
        {           
            db = new Model1();

            DateTime startDate = new DateTime(2017, 06, 01);            
            DateTime endDate = new DateTime(2017, 08, 30);
            

            var query = db.Timer.Where(w => w.AreaId == 38 && w.AreaId == 39 && w.AreaId == 102).Select(s => s);
            var query1 = query.Where(w => w.DateStart >= startDate && w.DateStart <= endDate).Select(s => s);
            var query2 = query.Where(w => w.DateFinish != null).Select(s => s);

            foreach (var item in query2)
            {
                Console.WriteLine("TimerId = {0}, User Id ={1}, Area ID = {2}, Date Start = {3}", item.TimerId, item.UserId, item.AreaId, item.DateStart);
            }
        }

        static void SqlToDataMethod9()
        {
            db = new Model1();

            var list = db.Timer.ToArray();

            int query = (from i in list where i.DateFinish != null select i).Count();

            Console.WriteLine(query);           
        }

        static void SqlToDataMethod10()
        {
            db = new Model1();

            var result = db.Area.Join(db.Timer, 
                                         p => p.AreaId, 
                                         t => t.AreaId, 
                                         (p, t) => new { AreaId = p.AreaId, Name = p.Name, TimerId = t.TimerId, DateStart = t.DateStart }); 

            foreach (var item in result)
            {
                Console.WriteLine("AreaId = {0}, Name = {1}, TimerId={2}, DateStart={3}", item.AreaId, item.Name, item.TimerId, item.DateStart);
            }
        }

        static void SqlToDataMethod11()
        {
            db = new Model1();

            var result = db.Area.GroupJoin(db.Timer,
                                         p => p.AreaId,
                                         t => t.AreaId,
                                         (area, timer) => new { AreaId = area.AreaId, Name = area.Name, Timer = timer.Select(s=>s.TimerId) });


            foreach (var item in result)
            {
                Console.WriteLine(item.AreaId);
                foreach (var i in item.Timer)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();
            }
            
        }

        static void SqlToDataMethod12()
        {
            db = new Model1();

            var result = db.Area.Join(db.Timer,
                                         p => p.AreaId,
                                         t => t.AreaId,
                                         (p, t) => new { AreaId = p.AreaId, Name = p.Name, TimerId = t.TimerId, DateStart = t.DateStart });

            var result1 = result.OrderByDescending(o => o.DateStart);

            foreach (var item in result1)
            {
                Console.WriteLine("AreaId = {0}, Name = {1}, TimerId={2}, DateStart={3}", item.AreaId, item.Name, item.TimerId, item.DateStart);
            }
        }

    }
}
