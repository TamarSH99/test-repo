using my_project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace my_project
{


    public class Listener
    {
        // fields
        public string listener_id;
        //public string ListenerId { get; set; }
        public string favorite_song_id;
        public long listening_number;

        public Listener()
        {

        }

        public Listener(string listenerID, string songID, long listeningNumber)
        {
            listener_id = listenerID;
            favorite_song_id = songID;
            listening_number = listeningNumber;
        }

        /*
                  public Listener(string listenerID, string songID, long listeningNumber )
        {
            Dictionary<string, Dictionary<string, long>> listener_dct = new Dictionary<string, Dictionary<string, long>>()
            {
                { listenerID, new Dictionary<string,long>() { { songID, listeningNumber }
            };

        }
         */

        // methods
        public void print_ObjectInfo()
        {
            Console.WriteLine("Class Listener");
            Console.WriteLine($" Listener ID => {listener_id}\n Song ID => {favorite_song_id}\n Listening number => {listening_number}");
        }

        public static List<Listener> read_txt()
        {
            string line;
            List<Listener> listener_list = new List<Listener>();

            string path = @"C:\Users\t.sharabidze\source\repos\my_project\my_project\listener_info.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split();
                Listener tmp = new Listener(data[0], data[1], Convert.ToInt64(data[2]));
                listener_list.Add(tmp);
            }
            file.Close();

            return listener_list;
        }


    }


    public class Musician : Listener
    {
        // fields
        public string song_id;
        public string title;
        public string artist_name;


        //constructor
        public Musician(string songID, string songTitle, string artistName)
        {
            song_id = songID;
            title = songTitle;
            artist_name = artistName;
        }

        //methods
        public void print_MusicianInfo()
        {
            Console.WriteLine("Class Musician:");
            Console.WriteLine($" Song ID => {song_id}\n Title => {title}\n Artist name => {artist_name}");
        }



        public static List<Musician> read_csv()
        {
            string line;
            List<Musician> musician_list = new List<Musician>();


            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\t.sharabidze\source\repos\my_project\my_project\song_data.csv");
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                Musician tmp = new Musician(data[0], data[1], data[3]);
                musician_list.Add(tmp);
            }
            file.Close();

            return musician_list;
        }

        public static Dictionary<string, int> songStatistic(ref List<Listener> listener_list, ref List<Musician> musician_list)
        {
            IDictionary<string, int> pop = new Dictionary<string, int>();

            int length_mus = musician_list.Count, length_lis = listener_list.Count;

            for (int i = 0; i < length_mus; i++)
            {
                for (int j = 0; j < length_lis; j++)
                {
                    int number = 0;
                    if (musician_list[i].song_id == listener_list[j].favorite_song_id)
                    {
                        string song_name = musician_list[i].title;
                        number = (int)listener_list[j].listening_number;
                        if (pop.ContainsKey(song_name))
                            pop[song_name] = (pop[song_name] + number);
                        else
                            pop[song_name] = number;
                    }

                }

            }

            return (Dictionary<string, int>)pop;

        }


        public static Dictionary<string, int> singerStatistic(ref List<Listener> listener_list, ref List<Musician> musician_list)
        {
            IDictionary<string, int> pop_singer = new Dictionary<string, int>();
            int length_mus = musician_list.Count, length_lis = listener_list.Count;

            for (int i = 0; i < length_mus; i++)
            {
                for (int j = 0; j < length_lis; j++)
                {
                    int number = 0;
                    if (musician_list[i].song_id == listener_list[j].favorite_song_id)
                    {
                        string singer_name = musician_list[i].artist_name;
                        number = (int)listener_list[j].listening_number;
                        if (pop_singer.ContainsKey(singer_name))
                            pop_singer[singer_name] = (pop_singer[singer_name] + number);
                        else
                            pop_singer[singer_name] = number;
                    }

                }

            }

            return (Dictionary<string, int>)pop_singer;

        }

        public static void print_statistic(Dictionary<string, int> pop)
        {
            var sortedDict = from entry in pop orderby entry.Value ascending select entry;
            Console.WriteLine(string.Concat(Enumerable.Repeat("#", 80)));

            foreach (var item in sortedDict)
            {
                int width = 80 - item.Key.Length;
                Console.WriteLine(item.Key + string.Concat(Enumerable.Repeat("_", width)) + item.Value);
            }

        }

        public static void dialog(ref List<Listener> listener_list, ref List<Musician> musician_list)
        {
            string name;
            string answer;
            Dictionary<string, int> pop;
            Console.WriteLine("What's your name ?");
            name = Console.ReadLine();
            Console.WriteLine($"Hi, {name} ! \n Click a if you are interested in the list of popular singers \n " +
                                                $"Click b if you are interested in a list of popular favorites");
            answer = Console.ReadLine();
            switch (answer)
            {
                case "a":
                    pop =
                  Musician.singerStatistic(ref listener_list, ref musician_list);
                    Musician.print_statistic(pop);
                    break;
                case "b":
                    pop =
                   Musician.songStatistic(ref listener_list, ref musician_list);
                    Musician.print_statistic(pop);
                    break;
                default: Console.WriteLine("Try again"); break;
            }




        }

    }

}


class Program
{
    static void Main(string[] args)
    {
        List<Listener> listener_list = Listener.read_txt();
        List<Musician> musician_list = Musician.read_csv();

        Musician.dialog(ref listener_list, ref musician_list);

        //   listener_list[i].print_ObjectInfo();
        //   musician_list[i].print_MusicianInfo();

        //   Dictionary<string, int> pop_song = Musician.songStatistic(ref listener_list, ref musician_list);
        //   Dictionary<string, int> pop_singer = Musician.singerStatistic(ref listener_list, ref musician_list);
        //   Musician.print_statistic(pop_song);
        //   Musician.print_statistic(pop_singer);
    }
}




