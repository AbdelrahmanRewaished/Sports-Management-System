using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sports_Management_System.Pages.Auth;
using System.Configuration;

namespace Sports_Management_System.Models;

public partial class ChampionsLeagueDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ChampionsLeagueDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ChampionsLeagueDbContext(DbContextOptions<ChampionsLeagueDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    public virtual DbSet<MatchView> AllMatches { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ClubRepresentative> ClubRepresentatives { get; set; }

    public virtual DbSet<ClubsNeverMatched> ClubsNeverMatcheds { get; set; }

    public virtual DbSet<Fan> Fans { get; set; }

    public virtual DbSet<HostRequest> HostRequests { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<SportsAssociationManager> SportsAssociationManagers { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<StadiumManager> StadiumManagers { get; set; }

    public virtual DbSet<SystemAdmin> SystemAdmins { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("allMatches");

            entity.Property(e => e.GuestClub)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("guest_club");
            entity.Property(e => e.HostClub)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("host_club");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.EndTime)
                .HasColumnName("datetime")
                .HasColumnName("end_time");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__System_U__F3DBC57378AE01A7");

            entity.ToTable("System_User2");

            entity.Property(e => e.Username)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__Club__BCBEC2A12A2D862A");

            entity.ToTable("Club");

            entity.HasIndex(e => e.Name, "UQ__Club__72E12F1BC8FEB7EC").IsUnique();

            entity.Property(e => e.ClubId).HasColumnName("club_ID");
            entity.Property(e => e.Location)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("club1_name");
            entity.Property(e => e.Club2Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("club2_name");
        });

        modelBuilder.Entity<Fan>(entity =>
        {
            entity.HasKey(e => e.NationalId).HasName("PK__Fan__956FEDD430BB685E");

            entity.ToTable("Fan");

            entity.Property(e => e.NationalId)
                .ValueGeneratedNever()
                .HasMaxLength(20)
                .HasColumnName("national_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNo).HasMaxLength(20).HasColumnName("phone_no");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
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

        modelBuilder.Entity<AlreadyPlayedMatch>()
            .HasNoKey()
            .ToView("AlreadyPlayedMatches");

        modelBuilder.Entity<UpComingMatch>()
            .HasNoKey()
            .ToView("AllUpComingMatches");

        modelBuilder.Entity<SentRequest>()
            .HasNoKey();

        modelBuilder.Entity<AvailableMatch>()
            .HasNoKey();

        modelBuilder.Entity<AvailableStadium>()
            .HasNoKey();

        modelBuilder.Entity<PurchasedTicket>()
            .HasNoKey();

        modelBuilder.Entity<PendingRequest>()
            .HasNoKey();

        modelBuilder.Entity<UpComingMatchesOfClub>()
            .HasNoKey();

        modelBuilder.Entity<SportsAssociationManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sports_a__3214EC278C809F50");

            entity.ToTable("Sports_association_manager");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StadiumId).HasColumnName("stadium_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
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
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.SystemAdmins)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__System_Ad__usern__619B8048");
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

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
           .GetMethod(nameof(GetAllUpComingMatchesOfClub), new[] { typeof(int) }))
           .HasName("getAllUpComingMatchesOfClub");

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
            .GetMethod(nameof(GetAllHostRequestsSentBy), new[] { typeof(int) }))
            .HasName("getAllHostRequestsSentBy");

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
           .GetMethod(nameof(ViewAvailableStadiumsOn), new[] { typeof(string), typeof(string), typeof(DateTime) }))
           .HasName("viewAvailableStadiumsOn");

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
           .GetMethod(nameof(GetAvailableMatches)))
           .HasName("availableMatchesToAttend");

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
            .GetMethod(nameof(ViewPurchasedTickets), new[] {typeof(string)}))
            .HasName("purchasedTicketsPerMatchFor");

        modelBuilder.HasDbFunction(typeof(ChampionsLeagueDbContext)
           .GetMethod(nameof(GetPendingRequests), new[] { typeof(string) }))
           .HasName("allPendingRequests");

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    internal Stadium? Find(int? stadiumId)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public IQueryable<UpComingMatchesOfClub> GetAllUpComingMatchesOfClub(int clubId)
    => FromExpression(() => GetAllUpComingMatchesOfClub(clubId));

    public IQueryable<SentRequest> GetAllHostRequestsSentBy(int hostId)
        => FromExpression(() => GetAllHostRequestsSentBy(hostId));

    public IQueryable<AvailableStadium> ViewAvailableStadiumsOn(string hostClub, string guestClub, DateTime startTime)
        => FromExpression(() => ViewAvailableStadiumsOn(hostClub, guestClub, startTime));

    public IQueryable<AvailableMatch> GetAvailableMatches()
        => FromExpression(() => GetAvailableMatches());

    public IQueryable<PurchasedTicket> ViewPurchasedTickets(string nationalId)
        => FromExpression(() => ViewPurchasedTickets(nationalId));

    public IQueryable<PendingRequest> GetPendingRequests(string username)
        => FromExpression(() => GetPendingRequests(username));

    public async Task<int> GetClubsCount()
    {
        return await Clubs.CountAsync();
    }

    public async Task<int> GetStadiumsCount()
    {
        return await Stadia.CountAsync();
    }

    public async Task<int> GetFansCount()
    {
        return await Fans.CountAsync();
    }
    public async Task<int> GetCreatedMatchesCount() 
    {
        return await Matches.CountAsync();
    }
    public async Task<int> GetAlreadyPlayedMatchesCount()
    {
        return await Set<AlreadyPlayedMatch>().CountAsync();
    }

    public async Task<int> GetUpcomingMatchesCount() 
    {
        return await Set<UpComingMatch>().CountAsync();
    }

    public async Task<int> GetMatchesPendingHostingCount(string username) 
    {
        int representativeId = (await GetCurrentClubRepresentative(username)!).Id;
        return HostRequests
            .Where(n => n.RepresentativeId == representativeId && n.Status == "unhandled").Count();
    }

    public async Task<int> GetTotallyHostedMatches(string username)
    {
        int representativeId = (await GetCurrentClubRepresentative(username)!).Id;
        return HostRequests
            .Where(n => n.RepresentativeId == representativeId && n.Status == "accepted").Count();
    }
    public async Task<int> GetTotalPendingRequests(string username)
    {
        return await GetPendingRequests(username).CountAsync();
    }

    public async Task<int> GetTotalAvailableMatches()
    {
        return await GetAvailableMatches().CountAsync();
    }

    public async Task<int> GetTotallyBoughtTickets(string username)
    {
        string nationalId = (await GetCurrentFan(username)).NationalId;
        return await ViewPurchasedTickets(nationalId).CountAsync();
    }

    public async Task<ClubRepresentative> GetCurrentClubRepresentative(string username)
    {
        return await ClubRepresentatives.FirstOrDefaultAsync(n => n.Username == username);
    }

    public async Task<Fan> GetCurrentFan(string username)
    {
        return await Fans.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<StadiumManager> GetCurrentStadiumManager(string username)
    {
        return await StadiumManagers.FirstOrDefaultAsync(u => u.Username == username);
    }

    public bool IsClubExisting(string clubName)
    {
        return ! Clubs.Where(n => n.Name == clubName).IsNullOrEmpty();
    }

    public async Task<int> GetClubId(string name)
    {
        return (await Clubs.FirstOrDefaultAsync(u => u.Name == name)).ClubId;
    }

    public async Task<int> GetMatchIdAsync(string hostClub, string guestClub, DateTime startTime)
    {
        int hostId = await GetClubId(hostClub);
        int guestId = await GetClubId(guestClub);
        return (await Matches.FirstOrDefaultAsync(n => 
        n.HostClubId == hostId &&
        n.GuestClubId == guestId &&
        n.StartTime == startTime)).MatchId;
    }
    private async Task<int> GetStadiumId(string name)
    {
        return (await Stadia.FirstOrDefaultAsync(n => n.Name == name)).Id;
    }

    public async Task<StadiumManager> GetStadiumManager(string stadiumName)
    {
        int stadiumId = await GetStadiumId(stadiumName);
        return await StadiumManagers.FirstOrDefaultAsync(n => n.StadiumId == stadiumId);
    }

    public async Task<ClubRepresentative> GetClubRepresentative(string clubName)
    {
        int clubId = await GetClubId(clubName);
        return await ClubRepresentatives.FirstOrDefaultAsync(n => n.ClubId == clubId);
    }
    public async Task<SystemUser?> GetStadiumManagerAsUser(string stadiumName)
    {
        var StadiumManager = await GetStadiumManager(stadiumName);
        if(StadiumManager == null)
        {
            return null;
        }
        return await (SystemUsers.FindAsync(StadiumManager.Username!))!;
    }

    public async Task<SystemUser?> GetClubRepresentativeAsUser(int clubId)
    {
        var ClubRepresentative = ClubRepresentatives.FirstOrDefault(u => u.ClubId == clubId)!;
        if(ClubRepresentative == null)
        {
            return null;
        }
        return await SystemUsers.FindAsync(ClubRepresentative.Username!)!;
    }
    public async Task<bool?> IsRequestRejected(string username, string hostClub, string guestClub, DateTime startTime, string stadium)
    {
        ClubRepresentative clubRepresentative = await GetCurrentClubRepresentative(username);

        int matchId = await GetMatchIdAsync(hostClub, guestClub, startTime);

        int stadiumManagerId = (await GetStadiumManager(stadium)).Id;

        var Request = await HostRequests.FirstOrDefaultAsync(n =>
        n.ManagerId == stadiumManagerId &&
        n.MatchId == matchId &&
        n.Status == "rejected");
                                              
        return Request != null;
    }

    public async Task<bool> IsMatchHostableAsync(string repUsername, string hostClub,
        string guestClub, DateTime startTime)
    {
        int matchId = await GetMatchIdAsync(hostClub, guestClub, startTime);
        ClubRepresentative clubRepresentative = await GetCurrentClubRepresentative(repUsername);

        var Requests = HostRequests.Where(n => n.MatchId == matchId)
            .Where(n => n.RepresentativeId == clubRepresentative.Id);
        if(Requests.IsNullOrEmpty()) {
            return true;
        }
        return Requests.Where(n => n.Status == "unhandled" || n.Status == "accepted").IsNullOrEmpty();
    }

    public async Task<bool> IsAssociationManager(string username)
    {
        var assocManager = await SportsAssociationManagers.FirstOrDefaultAsync(u => u.Username == username);
        return assocManager != null;
    }
    public async Task<bool> IsStadiumManager(string username)
    {
        var stadiumManager = await StadiumManagers.FirstOrDefaultAsync(u => u.Username == username);
        return stadiumManager != null;
    }
    public async Task<bool> IsClubRepresentative(string username)
    {
        var clubRepresentative = await ClubRepresentatives.FirstOrDefaultAsync(u => u.Username == username);
        return clubRepresentative != null;
    }
    public async Task<bool> IsAdmin(string username)
    {
        var admin = await SystemAdmins.FirstOrDefaultAsync(u => u.Username == username);
        return admin != null;
    }
}
