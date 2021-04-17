<template>
  <v-container fluid>
    <v-row>
      <v-col cols="6" offset="3">
        <div class="text-right" v-if="$logged.role == 'Admin'">
          <v-tooltip left>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                @click="add"
                class="ma-2"
                color="#4CAF"
                v-bind="attrs"
                v-on="on"
              >
                <v-icon large color="white">mdi-account-plus</v-icon>
              </v-btn>
            </template>
            <span>Dodaj studenta</span>
          </v-tooltip>
        </div>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">
                  Broj indeksa
                </th>
                <th class="text-left">
                  Ime i prezime
                </th>
                <th class="text-left">
                  Godina
                </th>
                <th class="text-left">
                  Status studenta
                </th>
                <th></th>
                <th v-if="$logged.role == 'Admin'"></th>
                <th v-if="$logged.role == 'Admin'"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="student in students" :key="student.studentId">
                <td>{{ student.brojIndeksa }}</td>
                <td>{{ student.ime }} {{ student.prezime }}</td>
                <td>{{ student.godina }}</td>
                <td>{{ student.nazivStatusa }}</td>
                <td>
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        icon
                        v-bind="attrs"
                        v-on="on"
                        @click="details(student)"
                      >
                        <v-icon>mdi-account-details</v-icon>
                      </v-btn>
                    </template>
                    <span>Detalji</span>
                  </v-tooltip>
                </td>
                <td v-if="$logged.role == 'Admin'">
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        @click="edit(student)"
                        icon
                        v-bind="attrs"
                        v-on="on"
                      >
                        <v-icon>mdi-account-edit</v-icon>
                      </v-btn>
                    </template>
                    <span>Izmjena</span>
                  </v-tooltip>
                </td>
                <td v-if="$logged.role == 'Admin'">
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        icon
                        @click="del(student)"
                        v-bind="attrs"
                        v-on="on"
                      >
                        <v-icon>mdi-delete</v-icon>
                      </v-btn>
                    </template>
                    <span>Brisanje</span>
                  </v-tooltip>
                </td>
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
    };
  },
  async created() {
    this.$token = "Bearer " + localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (this.$logged == null) {
      this.$router.push("/login");
    } else {
      await this.$axios
        .get("api/students/getstudentsprocedure", {
          headers: { Authorization: this.$token },
        })
        .then((response) => {
          this.students = response.data;
        });
    }
  },
  async updated() {
    this.$axios.get("api/students/getstudentsprocedure").then((response) => {
      this.students = response.data;
    });
  },
  methods: {
    details(student) {
      this.$router.push("/details-student/" + student.studentId);
    },
    edit(student) {
      this.$router.push("/edit-student/" + student.studentId);
    },
    add() {
      this.$router.push("/add-student");
    },
    async del(student) {
      this.$swal({
        text: "Da li želite da obrišete ovog studenta?",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "Ne",
        confirmButtonText: "Da",
        confirmButtonColor: "#4CAF",
        cancelButtonColor: "#E15757",
      }).then((res) => {
        if (res.value) {
          this.$axios
            .delete(`api/students/${student.studentId}`, {
              headers: {
                Authorization: this.$token,
              },
            })
            .then((response) => {
              this.students = response.data;
            });
        }
      });
    },
  },
};
</script>

<style></style>
