//
//  SyncCollectionToDb.cs
//
//  Author:
//       Faustino Lukolo <faustino.lukolo@wsu.edu>
//
//  Copyright (c) 2015 GPL
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Runtime.Remoting.Contexts;

namespace WearableSensorUI
{
    public class WearableJsonService : IWearableJsonService
    {
        private Context mContext;

        public WearableJsonService()
        {
            
        }

        #region IWearableActivityService implementation

        public void RefreshCache()
        {
            throw new NotImplementedException();
        }

        public IWearableJsonService GetActivity(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveActivity(WearableActivity myActivity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(WearableActivity myActivity)
        {
            throw new NotImplementedException();
        }

        public void SaveActivity(WearableJsonService myActivity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(WearableJsonService myActivity)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IReadOnlyList<WearableActivity> Activities
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

