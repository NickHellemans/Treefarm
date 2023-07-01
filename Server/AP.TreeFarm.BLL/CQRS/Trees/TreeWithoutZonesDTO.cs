﻿namespace AP.MyTreeFarm.Application.CQRS.Trees;

public class TreeWithoutZonesDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string InstructionsUrl { get; set; }
    public string QrCodeUrl { get; set; }
}