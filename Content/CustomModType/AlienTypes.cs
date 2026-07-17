namespace Ben10OmnitrixMod
{
    // Single source of truth for which alien forms exist, mirroring MistbornMod's MetalType
    // enum pattern. None = human/base form. Extension point for future rosters (Ultimate/
    // Omniverse aliens etc.) - only the classic 10 are wired up to content this pass.
    public enum AlienType
    {
        None,
        Heatblast,
        FourArms,
        XLR8,
        Diamondhead,
        GreyMatter,
        Stinkfly,
        Ripjaws,
        Upgrade,
        Ghostfreak,
        Wildmutt,
    }
}
