using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models.Match
{
    public enum MatchError
    {
        InvalidStartDate = 0,
        UserAlreadyJoinded = 1,
        JoiningDateExpired = 2,
        WantedPlayersLeftAmmountLessThanOne = 3,
        Unconfirmed = 4,
        JoiningToOwnMatch = 5,
        AlreadyConfirmed = 6,
        OccupiedField = 7
    }
}
