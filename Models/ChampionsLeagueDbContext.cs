using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sports_Management_System.Models;

public partial class ChampionsLeagueDbContext : DbContext
{
    public ChampionsLeagueDbContext()
    {
    }

    public ChampionsLeagueDbContext(DbContextOptions<ChampionsLeagueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllAssocManager> AllAssocManagers { get; set; }

    public virtual DbSet<AllClub> AllClubs { get; set; }

    public virtual DbSet<AllClubRepresentative> AllClubRepresentatives { get; set; }

    public virtual DbSet<AllFan> AllFans { get; set; }

    public virtual DbSet<AllMatch> AllMatches { get; set; }

    public virtual DbSet<AllRequest> AllRequests { get; set; }

    public virtual DbSet<AllStadium> AllStadiums { get; set; }

    public virtual DbSet<AllStadiumManager> AllStadiumManagers { get; set; }

    public virtual DbSet<AllTicket> AllTickets { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ClubRepresentative> ClubRepresentatives { get; set; }

    public virtual DbSet<ClubsNeverMatched> ClubsNeverMatcheds { get; set; }

    public virtual DbSet<ClubsWithNoMatch> ClubsWithNoMatches { get; set; }

    public virtual DbSet<Fan> Fans { get; set; }

    public virtual DbSet<HostRequest> HostRequests { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchesPerTeam> MatchesPerTeams { get; set; }

    public virtual DbSet<SportsAssociationManager> SportsAssociationManagers { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<StadiumManager> StadiumManagers { get; set; }

    public virtual DbSet<SystemAdmin> SystemAdmins { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LCOEQBE\\SQLEXPRESS;Database=Champions_league_Db;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllAssocManager>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allAssocManagers");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AllClub>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allClubs");

            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AllClubRepresentative>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allClubRepresentatives");

            entity.Property(e => e.ClubName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("club_name");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AllFan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allFans");

            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NationalId).HasColumnName("national_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AllMatch>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allMatches");

            entity.Property(e => e.GuestClub)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("guest_club");
            entity.Property(e => e.HostClub)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("host_club");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<AllRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allRequests");

            entity.Property(e => e.ClubRepresentative)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("club_representative");
            entity.Property(e => e.RequestStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("request_status");
            entity.Property(e => e.StadiumManager)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("stadium_manager");
        });

        modelBuilder.Entity<AllStadium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allStadiums");

            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<AllStadiumManager>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allStadiumManagers");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.StadiumName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("stadium_name");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AllTicket>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allTickets");

            entity.Property(e => e.GuestClub)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("guest_club");
            entity.Property(e => e.HostClub)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("host_club");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Club__BCBEC2A12A2D862A");

            entity.ToTable("Club");

            entity.HasIndex(e => e.Name, "UQ__Club__72E12F1BC8FEB7EC").IsUnique();

            entity.Property(e => e.ClubId).HasColumnName("club_ID");
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ClubRepresentative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Club_Rep__3214EC27395AF8E9");

            entity.ToTable("Club_Representative");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClubId).HasColumnName("club_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Club).WithMany(p => p.ClubRepresentatives)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Club_Repr__club___5BE2A6F2");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.ClubRepresentatives)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Club_Repr__usern__5AEE82B9");
        });

        modelBuilder.Entity<ClubsNeverMatched>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("clubsNeverMatched");

            entity.Property(e => e.Club1Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("club1_name");
            entity.Property(e => e.Club2Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("club2_name");
        });

        modelBuilder.Entity<ClubsWithNoMatch>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("clubsWithNoMatches");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Fan>(entity =>
        {
            entity.HasKey(e => e.NationalId).HasName("PK__Fan__956FEDD420BB685E");

            entity.ToTable("Fan");

            entity.Property(e => e.NationalId)
                .ValueGeneratedNever()
                .HasColumnName("national_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNo).HasColumnName("phone_no");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Fans)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fan__username__4E88ABD4");

            entity.HasMany(d => d.Tickets).WithMany(p => p.FanNationals)
                .UsingEntity<Dictionary<string, object>>(
                    "TicketBuyingTransaction",
                    r => r.HasOne<Ticket>().WithMany()
                        .HasForeignKey("TicketId")
                        .HasConstraintName("FK__Ticket_Bu__ticke__2A164134"),
                    l => l.HasOne<Fan>().WithMany()
                        .HasForeignKey("FanNationalId")
                        .HasConstraintName("FK__Ticket_Bu__fan_n__29221CFB"),
                    j =>
                    {
                        j.HasKey("FanNationalId", "TicketId").HasName("PK__Ticket_B__4D8EE9FBB63CC61D");
                        j.ToTable("Ticket_Buying_Transactions");
                    });
        });

        modelBuilder.Entity<HostRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Host_Req__3214EC27E16933A7");

            entity.ToTable("Host_Request");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ManagerId).HasColumnName("manager_ID");
            entity.Property(e => e.MatchId).HasColumnName("match_ID");
            entity.Property(e => e.RepresentativeId).HasColumnName("representative_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Manager).WithMany(p => p.HostRequests)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Host_Requ__manag__6A30C649");

            entity.HasOne(d => d.Match).WithMany(p => p.HostRequests)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Host_Requ__match__6B24EA82");

            entity.HasOne(d => d.Representative).WithMany(p => p.HostRequests)
                .HasForeignKey(d => d.RepresentativeId)
                .HasConstraintName("FK__Host_Requ__repre__693CA210");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Match__9D7ECC7BC993E263");

            entity.ToTable("Match");

            entity.Property(e => e.MatchId).HasColumnName("match_ID");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.GuestClubId).HasColumnName("guest_club_ID");
            entity.Property(e => e.HostClubId).HasColumnName("host_club_ID");
            entity.Property(e => e.StadiumId).HasColumnName("stadium_ID");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.GuestClub).WithMany(p => p.MatchGuestClubs)
                .HasForeignKey(d => d.GuestClubId)
                .HasConstraintName("FK__Match__guest_clu__66603565");

            entity.HasOne(d => d.HostClub).WithMany(p => p.MatchHostClubs)
                .HasForeignKey(d => d.HostClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Match__host_club__6477ECF3");

            entity.HasOne(d => d.Stadium).WithMany(p => p.Matches)
                .HasForeignKey(d => d.StadiumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Match__stadium_I__656C112C");
        });

        modelBuilder.Entity<MatchesPerTeam>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("matchesPerTeam");

            entity.Property(e => e.ClubName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("club_name");
            entity.Property(e => e.NoOfMatchesPlayed).HasColumnName("no_of_matches_played");
        });

        modelBuilder.Entity<SportsAssociationManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sports_a__3214EC278C809F50");

            entity.ToTable("Sports_association_manager");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.SportsAssociationManagers)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Sports_as__usern__5EBF139D");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium__3214EC27C8F30B3C");

            entity.ToTable("Stadium");

            entity.HasIndex(e => e.Name, "UQ__Stadium__72E12F1B940ED183").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<StadiumManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium___3214EC27C4E7CA6A");

            entity.ToTable("Stadium_Manager");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StadiumId).HasColumnName("stadium_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Stadium).WithMany(p => p.StadiumManagers)
                .HasForeignKey(d => d.StadiumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Stadium_M__stadi__5441852A");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.StadiumManagers)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Stadium_M__usern__5535A963");
        });

        modelBuilder.Entity<SystemAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__System_A__3214EC275C92340D");

            entity.ToTable("System_Admin");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.SystemAdmins)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__System_Ad__usern__619B8048");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__System_U__F3DBC57378AE01A7");

            entity.ToTable("System_User2");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC2727758A0B");

            entity.ToTable("Ticket");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatchId).HasColumnName("match_ID");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Ticket__match_ID__6E01572D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public int getStadiumManagerId(string stadium_name)
        => throw new NotSupportedException();

    public int getStadiumID(string stadium_name)
        => throw new NotSupportedException();
}
