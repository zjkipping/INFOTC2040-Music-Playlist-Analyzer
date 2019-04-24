using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlaylistAnalyzer {
  public class MusicPlaylist {
    private List<Song> songs;

    public MusicPlaylist() {
      songs = new List<Song>();
    }

    public MusicPlaylist(List<Song> songs) {
      this.songs = songs;
    }

    public void GenerateReport(string filePath) {
      if (songs.Count == 0) {
        throw new Exception("Song list is empty unable to generate report. Please check your data file and try again.");
      }
      StreamWriter sw = null;
      try {
        sw = new StreamWriter(filePath);

        sw.WriteLine("Music Playlist Report");
        sw.WriteLine();

        sw.WriteLine("Songs that received 200 or more plays:");
        IEnumerable<Song> highPlaySongs = from song in songs where song.Plays >= 200 select song;
        foreach (Song song in highPlaySongs) {
          sw.WriteLine(song);
        }

        sw.WriteLine();

        sw.WriteLine(string.Format("Number of Alternative songs: {0}", (from song in songs where song.Genre == "Alternative" select song).Count()));

        sw.WriteLine();

        sw.WriteLine(string.Format("Number of Hip-Hop/Rap songs: {0}", (from song in songs where song.Genre == "Hip-Hop/Rap" select song).Count()));

        sw.WriteLine();

        sw.WriteLine("Songs from the album Welcome to the Fishbowl:");
        IEnumerable<Song> specificAlbumSongs = from song in songs where song.Album == "Welcome to the Fishbowl" select song;
        foreach (Song song in specificAlbumSongs) {
          sw.WriteLine(song);
        }

        sw.WriteLine();

        sw.WriteLine("Songs from before 1970:");
        IEnumerable<Song> beforeCertainYearSongs = from song in songs where song.Year < 1970 select song;
        foreach (Song song in beforeCertainYearSongs) {
          sw.WriteLine(song);
        }

        sw.WriteLine();

        sw.WriteLine("Song names longer than 85 characters:");
        IEnumerable<Song> longNameSongs = from song in songs where song.Name.Length > 85 select song;
        foreach (Song song in longNameSongs) {
          sw.WriteLine(song);
        }

        sw.WriteLine();

        sw.WriteLine(string.Format("Longest song: {0}", (from song in songs orderby song.Time descending select song).FirstOrDefault()));
      } catch (Exception error) {
        throw error;
      } finally {
        if (sw != null) {
          sw.Close();
        }
      }
    }
  }
}
