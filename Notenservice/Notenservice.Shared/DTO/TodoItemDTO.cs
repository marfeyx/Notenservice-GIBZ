using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Todo.Shared.DTO;

public class TodoItemDTO
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Geben Sie bitte einen Todo-Namen an.")]
    [MaxLength(50, ErrorMessage = "Der Todo-Name darf nicht länger als 50 Zeichen sein.")]
    public string Name { get; set; }

    [MaxLength(100, ErrorMessage = "Die Todo-Beschreibung darf nicht länger als 100 Zeichen sein.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Geben Sie bitte einen Todo-Status an.")]
    public long StatusId { get; set; }

    [Required(ErrorMessage = "Geben Sie bitte eine Todo-Priorität an.")]
    public long PriorityId { get; set; }
}