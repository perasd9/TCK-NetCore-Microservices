import grpc from "k6/net/grpc";
import { check } from "k6";

export let options = {
    vus: 50,
    duration: "30s",
};

const client = new grpc.Client();
client.load(["."], "../Places.API/Core/Protos/place.proto");

let i = 0;

export default function () {
    if (i == 0) {
        i++;
        client.connect("localhost:9501", {});
    }

    let response = client.invoke("gRPCPlaceService/GetAll", {});

    check(response, {
        "status is OK": (r) => r && r.status === grpc.StatusOK,
    });
}
