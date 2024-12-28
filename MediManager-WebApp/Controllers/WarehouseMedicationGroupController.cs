    // Innerhalb der CreateEdit Action im Controller:
    // ViewBag.MedicationGroups erweitern um mehr Informationen:
    var availableGroups = await _context.MedicationGroups
        .Include(mg => mg.QuantityUnit)
        .Where(mg => !existingGroupIds.Contains(mg.ID))
        .OrderBy(mg => mg.Name)
        .Select(mg => new SelectListItem
        {
            Value = mg.ID.ToString(),
            Text = mg.Name,
            Group = new SelectListGroup { Name = mg.SupplyNumber },
            DataGroupField = mg.QuantityUnit.Name
        })
        .ToListAsync();

    ViewBag.MedicationGroups = availableGroups;