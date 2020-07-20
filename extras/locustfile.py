import random

from locust import HttpLocust, TaskSet, task


DNA_NUCLEOBASES = ["A", "C", "G", "T"]


class UserBehavior(TaskSet):
    @task(1)
    def stats(self):
        self.client.get("/stats/")

    @task(2)
    def is_mutant(self):
        dna_matrix = generate_dna_matrix(random.randint(4, 10))
        with self.client.post("/mutant/", json={"dna": dna_matrix}, catch_response=True) as response:
            if response.status_code == 403:
                response.success()


class WebsiteUser(HttpLocust):
    task_set = UserBehavior
    wait_time = 7


def generate_dna_matrix(size):
    return [
        "".join([
            DNA_NUCLEOBASES[random.randint(0, 3)] for _j in range(size)
        ]) for _i in range(size)
    ]