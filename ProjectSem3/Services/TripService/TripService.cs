﻿using ProjectSem3.DTOs;

namespace ProjectSem3.Services.TripService;

public interface TripService
{
    public bool Create(TripDTO tripDTO);

    public List<TripDTO> GetAll();

    public bool Update(TripDTO tripDTO);

    public bool Delete(int id);

}
