using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class VanInspection
{
    public int Id { get; set; }

    public string? ContainerNumber { get; set; }

    public DateTime? Eirdate { get; set; }

    public string? ShippingLine { get; set; }

    public string? Virnumber { get; set; }

    public int? Size { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? DateTime { get; set; }

    public string? TruckerName { get; set; }

    public string? PlateNumber { get; set; }

    public string? Driver { get; set; }

    public string? ColorCoding { get; set; }

    public string? Remarks { get; set; }

    public string? InspectedBy { get; set; }

    public string? Shipper { get; set; }

    public string? Customer { get; set; }

    public string? Location { get; set; }

    public string? ConfirmedBy { get; set; }

    public short HasChecklist { get; set; }

    public short IsInitial { get; set; }

    public int BookingId { get; set; }

    public short IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<CvChecklist> CvChecklists { get; set; } = new List<CvChecklist>();

    public virtual ICollection<VanInspectionChecklist> VanInspectionChecklists { get; set; } = new List<VanInspectionChecklist>();
}
