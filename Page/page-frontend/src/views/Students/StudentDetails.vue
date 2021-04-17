<template>
  <v-container class="student-details mt-2">
    <v-card elevation="4">
      <v-row>
        <v-col cols="10" offset="1">
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
          <div class="text-left" v-if="student != null">
            <p>
              <span class="font-weight-medium">Index: </span>
              <span class="font-weight-light">{{ student.brojIndeksa }}</span>
            </p>
            <p>
              <span class="font-weight-medium">Ime i prezime: </span>
              <span class="font-weight-light"
                >{{ student.ime }} {{ student.prezime }}</span
              >
            </p>
            <p>
              <span class="font-weight-medium">Godina studija: </span>
              <span class="font-weight-light">{{ student.godina }}</span>
            </p>
            <p>
              <span class="font-weight-medium">Status studenta: </span>
              <span class="font-weight-light">{{ student.nazivStatusa }}</span>
            </p>
            <p>
              <span class="font-weight-medium"
                >Kursevi na koje je upisan:
              </span>
            </p>
          </div>
          <v-simple-table>
            <template v-slot:default>
              <thead>
                <tr>
                  <th>#</th>
                  <th class="text-left">
                    Naziv kursa
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(kurs, index) in kursevi" :key="index">
                  <td>{{ index + 1 }}</td>
                  <td>{{ kurs.naziv }}</td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </v-col>
      </v-row>
    </v-card>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      student: {
        ime: "",
        prezime: "",
        brojIndeksa: "",
        nazivStatusa: "",
        godina: "",
      },
      kursevi: [],
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
        .get(`api/students/GetStudentByIdProcedure/${id}`, {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.student = response.data[0];
          if (this.student == null) {
            this.$router.push("/error");
          }
        });
      await this.$axios
        .get(`api/kursStudents/ks/${id}`, {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.kursevi = response.data;
        });
    }
  },
  methods: {
    goBack() {
      this.$router.push("/get-students");
    },
  },
};
</script>

<style>
.container.student-details {
  max-width: 600px;
}
</style>
