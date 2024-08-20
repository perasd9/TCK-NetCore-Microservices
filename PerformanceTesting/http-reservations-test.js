import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  vus: 30,
  duration: '30s',
};

export default function () {

  let params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };


  let res = http.post('https://localhost:9401/api/v1/reservations',  JSON.stringify({
    "reservationId": "091b4906-a202-44e3-9a70-a2c0d49e2854",
  "sumPrice": 1000,
  "dateOfReservation": "2024-08-15T15:29:05.174Z",
  "userId": "4fa45f64-5717-4562-b3fc-2c963f66afa6",
  "reservationComponents": [
    {
      "reservationId": "091b4906-a202-44e3-9a70-a2c0d49e2854",
      "serialNumber": 0,
      "price": 1000,
      "numberOfTickets": 1,
      "sumComponentPrice": 1000,
      "sportingEventId": "b08bbc24-5096-4ec9-99c4-675eda8c0e33"
    }
  ]
}), params);





  check(res, {
    'status is 200': (r) => r.status === 200,
  });
  //sleep(0.0001);
}