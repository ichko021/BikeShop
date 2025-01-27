//using BikeShop.DL.Interfaces;
//using BikeShop.DTO;

//namespace BikeShop.DL.Repositories
//{
//    public class BikeStaticRepository : IBikeRepository
//    {
//        public void AddBike(Bike bike)
//        {
//            InMemoryDB.listOfBikes.Add(bike);
//        }

//        public void DeleteBikeById(string id)
//        {
//            var bike = InMemoryDB.listOfBikes.FirstOrDefault(b => b.id.Equals(id));

//            if (bike != null)
//            {
//                InMemoryDB.listOfBikes.Remove(bike);
//            }
//            //foreach (Bike b in InMemoryDB.listOfBikes)
//            //{
//            //    if(b.id == id)
//            //    {
//            //        InMemoryDB.listOfBikes.Remove(b);
//            //    }
//            //}
//        }

//        public List<Bike> GetAllBikes()
//        {
//            return InMemoryDB.listOfBikes;
//        }

//        public Bike? GetBikeById(string id)
//        {
//            var bike = InMemoryDB.listOfBikes.FirstOrDefault(b => b.id.Equals(id));

//            if (bike != null)
//            {
//                return InMemoryDB.listOfBikes[bike.id];
//            }

//            return null;
//            //foreach (Bike b in InMemoryDB.listOfBikes)
//            //{
//            //    if (b.id == id)
//            //    {
//            //        return InMemoryDB.listOfBikes[b.id];
//            //    }
//            //}

//            //return null;
//        }

//        public void UpdateBikeById(int id, Bike bike)
//        {
//            var bikeRes = InMemoryDB.listOfBikes.FirstOrDefault(b => b.id == id);

//            if (bikeRes != null)
//            {
//                InMemoryDB.listOfBikes[bikeRes.id] = bike;
//            }

//            //foreach (Bike b in InMemoryDB.listOfBikes)
//            //{
//            //    if (b.id == id)
//            //    {
//            //        InMemoryDB.listOfBikes[id] = bike;
//            //    }
//            //}
//        }
//    }
//}
