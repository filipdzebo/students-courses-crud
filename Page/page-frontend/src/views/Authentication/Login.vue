<template>
  <div class="login">
    <v-app dark id="inspire">
      <v-container>
        <v-layout wrap>
          <v-flex sm12 md6 offset-md3>
            <v-card class="login-card" elevation="10" light tag="section">
              <v-card-title>
                <v-layout align-center justify-space-between>
                  <h3 class="headline font-weight-light">
                    Page
                  </h3>
                </v-layout>
              </v-card-title>
              <v-divider></v-divider>
              <v-card-text>
                <ValidationObserver ref="observer">
                  <v-form @submit.prevent="login">
                    <v-container fluid>
                      <ValidationProvider
                        name="Korisničko ime"
                        rules="required|max:12"
                        v-slot="{ errors }"
                      >
                        <v-text-field
                          v-model="user.username"
                          :error-messages="errors"
                          label="Korisničko ime"
                          required
                        >
                        </v-text-field>
                      </ValidationProvider>
                      <ValidationProvider
                        name="Lozinka"
                        rules="required|max:12|min:6"
                        v-slot="{ errors }"
                      >
                        <v-text-field
                          v-model="user.password"
                          label="Lozinka"
                          required
                          :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                          @click:append="show = !show"
                          :type="show ? 'text' : 'password'"
                          :error-messages="errors"
                          class="mb-4"
                        >
                        </v-text-field>
                      </ValidationProvider>
                      <v-btn type="submit" class="mr-4" color="#4CAF" dark>
                        Prijavi se
                      </v-btn>
                      <v-btn
                        @click="clear"
                        :disabled="formClear"
                        color="#4CAF"
                        dark
                      >
                        Očisti formu
                      </v-btn>
                    </v-container>
                  </v-form>
                </ValidationObserver>
              </v-card-text>
              <v-divider></v-divider>
              <v-card-actions>
                <div class="align-left ml-5">
                  <v-btn color="grey" dark @click="register">
                    Registracija
                  </v-btn>
                </div>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-app>
  </div>
</template>

<script>
import md5 from "js-md5";
import { required, min, max } from "vee-validate/dist/rules";
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
extend("max", {
  ...max,
  message: "{_field_} ne može biti duža od {length} karaktera.",
});
extend("min", {
  ...min,
  message: "{_field_} ne može biti kraća od {length} karaktera.",
});

export default {
  components: {
    ValidationObserver,
    ValidationProvider,
  },
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      show: false,
      validation: null,
    };
  },
  computed: {
    formClear() {
      return this.user.username != "" || this.user.password != ""
        ? false
        : true;
    },
  },
  methods: {
    async login() {
      this.validation = await this.$refs.observer.validate();
      if (!this.validation) {
        this.$swal({
          title: "Greška!",
          text: "Neispravno unešeni podaci.",
          icon: "warning",
        });
      } else {
        await this.$axios
          .post("api/auth/login", this.user)
          .then((response) => {
            if (response.data.success) {
              let token = response.data.data;
              localStorage.setItem("token", token);
              this.$token = "Bearer " + token;
              this.$login = true;
              this.$axios
                .get("api/auth/current-user", {
                  headers: {
                    Authorization: this.$token,
                  },
                })
                .then((response) => {
                  let user = response.data.data;
                  localStorage.setItem("user", JSON.stringify(user));
                  this.$swal({
                    toast: true,
                    animation: false,
                    showConfirmButton: false,
                    position: "top-right",
                    timer: 2000,
                    title: "Uspješno ste ulogovani!",
                    text: "Dobrodošli, " + user.username,
                    icon: "success",
                  }).then(this.$router.push("/"));
                })
                .catch((error) => console.log(error));
            } else {
              this.$swal({
                title: "Greška!",
                text: response.data.message,
                icon: "error",
              });
            }
          })
          .catch((error) => console.log(error));
      }
    },
    clear() {
      (this.user.username = ""), (this.user.password = ""), (this.show = false);
      this.$refs.observer.reset();
      this.validation = null;
    },
    async register() {
      this.validation = await this.$refs.observer.validate();
      if (!this.validation) {
        this.$swal({
          title: "Greška!",
          text: "Neispravno unešeni podaci.",
          icon: "warning",
        });
      } else {
        await this.$axios
          .post("api/auth/register", this.user)
          .then((response) => {
            if (response.data.success) {
              this.$swal({
                title: response.data.message,
                text: "Uspješna registracija",
                icon: "success",
                confirmButtonText: "Da",
                confirmButtonColor: "#6FCB58",
              });
            } else {
              this.$swal({
                title: "Greška!",
                text: response.data.message,
                icon: "error",
              });
            }
            this.clear();
          })
          .catch((error) => console.log(error));
      }
    },
  },
  created() {
    if (
      localStorage.getItem("token") != null ||
      localStorage.getItem("user") != null
    ) {
      this.$router.push("/");
    }
  },
};
</script>

<style>
.v-btn,
.v-card {
  border-radius: 10px !important;
}
.v-card__title {
  text-transform: uppercase;
}
.theme--light.v-application {
  background-image: url("../../assets/background-login.jpg");
  background-size: cover;
}
.theme--light.v-card.login-card {
  margin-top: 18%;
}
</style>
