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
                <v-icon large color="white">mdi-book-open-variant</v-icon>
              </v-btn>
            </template>
            <span>Dodaj kurs</span>
          </v-tooltip>
        </div>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">
                  #
                </th>
                <th class="text-left">
                  Naziv kursa
                </th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(kurs, index) in kursevi" :key="index">
                <td>{{ index + 1 }}</td>
                <td>{{ kurs.naziv }}</td>
                <td>
                  <v-tooltip bottom>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn
                        icon
                        v-bind="attrs"
                        v-on="on"
                        @click="details(kurs)"
                      >
                        <v-icon>mdi-dots-horizontal</v-icon>
                      </v-btn>
                    </template>
                    <span>Detalji</span>
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
      kursevi: [],
    };
  },
  methods: {
    add() {
      this.$router.push("/add-course");
    },
    details(kurs) {
      this.$router.push("/details-course/" + kurs.kursId);
    },
  },
  async created() {
    this.$token = "Bearer " + localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (this.$logged == null) {
      this.$router.push("/login");
    } else {
      await this.$axios
        .get("api/kurs", {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.kursevi = response.data;
        });
    }
  },
};
</script>

<style></style>
