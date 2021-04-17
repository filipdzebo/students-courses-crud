<template>
  <v-app id="inspire">
    <div v-if="logged != null">
      <v-navigation-drawer permanent v-if="drawer" app>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="title"> PAGE </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list dense nav>
          <v-list-item
            v-for="item in items"
            :key="item.title"
            :to="item.to"
            link
          >
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>

      <v-app-bar app color="green" dark>
        <template v-slot:img="{ props }">
          <v-img
            v-bind="props"
            gradient="to top right, rgba(19,84,122,.5), rgba(128,208,199,.8)"
          ></v-img>
        </template>

        <v-app-bar-nav-icon v-on:click="toggleNavBar"></v-app-bar-nav-icon>

        <v-toolbar-title>PAGE</v-toolbar-title>

        <v-spacer></v-spacer>
        <v-btn color="#4CAF" @click="logout">
          Logout
          <v-icon>mdi-logout</v-icon>
        </v-btn>
      </v-app-bar>
    </div>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",
  components: {},
  data() {
    return {
      logged: "",
      drawer: null,
      items: [
        { title: "Početna", icon: "mdi-home", to: "/" },
        {
          title: "Lista studenata",
          icon: "mdi-format-list-bulleted",
          to: "/get-students",
        },
        {
          title: "Lista kurseva",
          icon: "mdi-format-list-bulleted-triangle",
          to: "/get-courses",
        },
      ],
    };
  },
  methods: {
    toggleNavBar() {
      this.drawer = !this.drawer;
    },
    logout() {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      delete this.$axios.defaults.headers.common["Authorization"];
      this.$router.push("/login");
    },
  },
  created() {
    this.logged = localStorage.getItem("token");
  },
  updated() {
    this.logged = localStorage.getItem("token");
  },
};
</script>

<style scoped>
/* .theme--light.v-application {
  background-image: url("./assets/background-login.jpg") !important;
  background-size: cover;
} */
</style>
