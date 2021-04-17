<template>
  <v-container class="edit-student">
    <v-card elevation="4">
      <ValidationObserver ref="observer">
        <v-form @submit.prevent="addOrEdit">
          <v-container fluid>
            <v-row>
              <v-col cols="10" offset="1">
                <ValidationProvider
                  name="Ime"
                  v-slot="{ errors }"
                  rules="alpha"
                >
                  <v-text-field
                    v-model="student.ime"
                    label="Ime"
                    :error-messages="errors"
                  >
                  </v-text-field>
                </ValidationProvider>
                <ValidationProvider
                  name="Prezime"
                  v-slot="{ errors }"
                  rules="alpha"
                >
                  <v-text-field
                    v-model="student.prezime"
                    :error-messages="errors"
                    label="Prezime"
                  >
                  </v-text-field>
                </ValidationProvider>
                <v-select
                  :items="godine"
                  v-model="student.godina"
                  label="Godina Studija"
                ></v-select>
                <ValidationProvider
                  name="Status studenta"
                  rules="required"
                  v-slot="{ errors }"
                >
                  <v-select
                    v-model="student.statusStudenta"
                    :error-messages="errors"
                    label="Status studenta"
                    :items="statusi"
                    :item-text="statusi.text"
                    :item-value="statusi.value"
                    required
                  >
                  </v-select>
                </ValidationProvider>
                <ValidationProvider
                  name="Broj indeksa"
                  rules="required|between"
                  v-slot="{ errors }"
                >
                  <v-text-field
                    v-model="student.brojIndeksa"
                    label="Broj indeksa"
                    required
                    :error-messages="errors"
                    class="mb-5"
                  >
                  </v-text-field>
                </ValidationProvider>
                <v-btn type="submit" class="mr-4 mb-4" v-if="!$route.params.id">
                  Dodaj studenta
                </v-btn>
                <v-btn
                  type="submit"
                  class="mr-4 mb-4"
                  v-else-if="$route.params.id"
                >
                  Izmjeni studenta
                </v-btn>
                <v-btn @click="clear" :disabled="formClear" class="mr-4 mb-4">
                  Očisti formu
                </v-btn>
                <v-btn @click="goBack" color="#4CAF">
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
import { required, between, alpha } from "vee-validate/dist/rules";
import {
  extend,
  ValidationObserver,
  ValidationProvider,
  setInteractionMode,
} from "vee-validate";
setInteractionMode("passive");

extend("alpha", {
  ...alpha,
  message: "{_field_} mora sadržati samo slova.",
});
extend("required", {
  ...required,
  message: "{_field_} je obavezno polje.",
});
extend("between", {
  ...between,
  message: "{_field_} mora biti između 1000 u 10000.",
  validate: (value) => {
    if (value > 999 && value < 10000) {
      return true;
    }
    return false;
  },
});

export default {
  components: { ValidationObserver, ValidationProvider },
  data() {
    return {
      student: {
        ime: "",
        prezime: "",
        godina: 1,
        brojIndeksa: "",
        statusStudenta: "",
      },
      godine: [1, 2, 3, 4],
      statusi: [
        { value: 1, text: "Redovan" },
        { value: 2, text: "Vanredan" },
      ],
      validation: null,
      id: "",
    };
  },
  methods: {
    async addOrEdit() {
      this.validation = await this.$refs.observer.validate();
      if (!this.validation) {
        this.$swal({
          title: "Greška!",
          text: "Neispravno unešeni podaci.",
          icon: "warning",
        });
      } else {
        if (!this.$route.params.id) {
          await this.$axios
            .post("api/students", this.student, {
              headers: {
                Authorization: this.$token,
              },
            })
            .then((response) => {
              if (response.status == 201) {
                this.$swal({
                  confirmButtonColor: "#4CAF",
                  title: "Uspješno ste kreirali studenta!",
                  icon: "success",
                });
                this.clear();
              }
            })
            .catch((error) => console.log(error));
        } else {
          this.$axios
            .put(`api/students/${this.id}`, this.student, {
              headers: {
                Authorization: this.$token,
              },
            })
            .then((response) => {
              this.$swal({
                confirmButtonColor: "#4CAF",
                title: "Uspješno ste izmjenili studenta!",
                icon: "success",
              });
            });
        }
      }
    },
    clear() {
      this.student = {
        ime: "",
        prezime: "",
        brojIndeksa: "",
        godina: 1,
        statusStudenta: "",
      };
      this.$refs.observer.reset();
      this.validation = null;
    },
    goBack() {
      this.$router.push("/get-students");
    },
  },
  computed: {
    formClear() {
      return this.student.ime != "" ||
        this.student.prezime != "" ||
        this.student.brojIndeksa != "" ||
        this.student.statusStudenta != ""
        ? false
        : true;
    },
  },
  async created() {
    this.$token = "Bearer " + localStorage.getItem("token");
    this.$logged = JSON.parse(localStorage.getItem("user"));
    if (this.$logged == null) {
      this.$router.push("/login");
    } else if (this.$logged.role == "Korisnik") {
      this.$router.push("/auth-error");
    } else {
      if (this.$route.params.id) {
        this.id = this.$route.params.id;
        await this.$axios
          .get(
            `api/students/getstudentbyidprocedure/${this.$route.params.id}`,
            {
              headers: {
                Authorization: this.$token,
              },
            }
          )
          .then((response) => {
            if (response.data.length == 0) {
              this.$router.push("/error");
            }
            let status = response.data[0].nazivStatusa;
            this.student = response.data[0];
            this.student.statusStudenta = status == "Redovan" ? 1 : 2;
          });
      }
    }
  },
};
</script>

<style>
.container.edit-student {
  max-width: 500px;
}
</style>
