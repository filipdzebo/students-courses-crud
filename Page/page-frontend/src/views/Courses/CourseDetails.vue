<template>
  <v-container fluid>
    <v-row>
      <v-col cols="6" offset="3">
        <div class="text-right">
          <v-tooltip left>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                @click="goBack"
                class="ma-2"
                color="#4CAF"
                v-bind="attrs"
                v-on="on"
                dark
              >
                <v-icon color="white">mdi-backspace-reverse</v-icon>
              </v-btn>
            </template>
            <span>Vrati se na listu</span>
          </v-tooltip>
        </div>
        <div class="text-center mb-3 mt-3">
          <span class="display-1 font-weight-normal">{{ kurs }}</span>
        </div>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th>#</th>
                <th class="text-left">
                  Broj indeksa
                </th>
                <th class="text-left">
                  Ime i prezime
                </th>
                <th class="text-left">
                  Godina
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(student, index) in students" :key="index">
                <td>{{ index + 1 }}</td>
                <td>{{ student.brojIndeksa }}</td>
                <td>{{ student.ime }} {{ student.prezime }}</td>
                <td>{{ student.godina }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      students: [],
      kurs: "",
    };
  },
  async created() {
    this.$token = "Bearer " + localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (this.$logged == null) {
      this.$router.push("/login");
    } else {
      let id = this.$route.params.id;
      await this.$axios
        .get(`api/kurs/${id}`, {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.kurs = response.data.naziv;
        })
        .catch((error) => {
          console.log(error);
          this.$router.push("/error");
        });
      await this.$axios
        .get(`api/kursStudents/sk/${id}`, {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.students = response.data;
        });
    }
  },
  methods: {
    goBack() {
      this.$router.push("/get-courses");
    },
  },
};
</script>

<style></style>
