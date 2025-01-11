//     void RotateGun()
// {
//     if (zombieTarget == null) return;

//     // Tính hướng từ súng tới zombie
//     Vector3 directionToZombie = zombieTarget.position - transform.position;
//     float angle = Mathf.Atan2(directionToZombie.y, directionToZombie.x) * Mathf.Rad2Deg;

//     // Quay súng về phía zombie
//     Quaternion rotation = Quaternion.Euler(0, 0, angle);
//     transform.rotation = rotation;
// }