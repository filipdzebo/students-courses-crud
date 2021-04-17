<template>
  <v-container class="add-course">
    <v-card elevation="4">
      <ValidationObserver ref="observer">
        <v-form @submit.prevent="add">
          <v-container fluid>
            <v-row>
              <v-col cols="10" offset="1">
                <ValidationProvider
                  name="Naziv kursa"
                  rules="required|min:3"
                  v-slot="{ errors }"
                >
                  <v-text-field
                    v-model="kurs.naziv"
                    label="Naziv kursa"
                    required
                    :error-messages="errors"
                  >
                  </v-text-field>
                </ValidationProvider>
                <v-container fluid v-if="kurs.studenti.length">
                  <v-checkbox
                    v-for="student in kurs.studenti"
                    :key="student.studentId"
                    @click="updateStudents"
                    class="check-box-list"
                    :label="
                      student.ime +
                        ' ' +
                        student.prezime +
                        ' ' +
                        student.brojIndeksa
                    "
                    v-model="student.checked"
                    :disabled="kurs.naziv.length < 3"
                  >
                  </v-checkbox>
                </v-container>
                <v-btn type="submit" class="mr-4 mb-4">
                  Dodaj kurs
                </v-btn>
                <v-btn @click="clear" :disabled="formClear" class="mr-4 mb-4">
                  Očisti formu
                </v-btn>
                <v-btn @click="goBack" color="#4CAF" class="mb-4 mr-4">
                  <v-icon large color="white">mdi-exit-to-app</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
      </ValidationObserver>
    </v-card>
  </v-container>
</template>

<script>
import { required, min } from "vee-validate/dist/rules";
import {
  extend,
  ValidationObserver,
  ValidationProvider,
  setInteractionMode,
} from "vee-validate";
setInteractionMode("passive");

extend("required", {
  ...required,
  message: "{_field_} je obavezno polje.",
});
extend("min", {
  ...min,
  message: "{_field_} ne može biti kraći od {length} karaktera.",
});

export default {
  components: { ValidationObserver, ValidationProvider },
  data() {
    return {
      kurs: {
        naziv: "",
        studenti: [],
        odabraniStudenti: [],
      },
      validation: null,
    };
  },
  async created() {
    this.$token = "Bearer " + localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (this.$logged == null) {
      this.$router.push("/login");
    } else if (this.$logged.role == "Korisnik") {
      this.$router.push("/auth-error");
    } else {
      await this.$axios
        .get("api/students/getstudentsprocedure", {
          headers: {
            Authorization: this.$token,
          },
        })
        .then((response) => {
          this.kurs.studenti = response.data;
          this.kurs.studenti.forEach((element) => {
            element["checked"] = false;
          });
        });
    }
  },
  methods: {
    goBack() {
      this.$router.push("/get-courses");
    },
    clear() {
      this.$nextTick(() => {
        this.kurs.naziv = "";
        this.kurs.studenti.forEach((element) => {
          element["checked"] = false;
        });
        this.kurs.studenti.forEach((element) => {
          element["checked"] = false;
        });
        this.$refs.observer.reset();
        this.validation = null;
      });
    },
    async add() {
      this.validation = await this.$refs.observer.validate();
      if (!this.validation) {
        this.$swal({
          title: "Greška!",
          text: "Neispravno unešeni podaci.",
          icon: "warning",
        });
      } else {
        await this.$axios
          .post("api/kurs", this.kurs, {
            headers: {
              Authorization: this.$token,
            },
          })
          .then((response) => {
            if (response.status == 201) {
              this.$swal({
                confirmButtonColor: "#4CAF",
                title: "Uspješno ste kreirali kurs i dodali studente!",
                icon: "success",
              });
              this.clear();
            }
          });
      }
    },
    updateStudents() {
      let temp = this.kurs.studenti.filter((x) => x.checked == true);
      this.kurs.odabraniStudenti = [];
      temp.forEach((element) => {
        this.kurs.odabraniStudenti.push(element.studentId);
      });
    },
  },
  computed: {
    formClear() {
      return this.kurs.naziv != "" ? false : true;
    },
  },
};
</script>

<style>
.container.add-course {
  max-width: 500px;
}
</style>
