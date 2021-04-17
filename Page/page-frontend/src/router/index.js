import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import StudentList from '../views/Students/StudentList.vue'
import StudentEdit from '../views/Students/StudentEdit.vue'
import CourseList from '../views/Courses/CourseList.vue'
import CourseAdd from '../views/Courses/CourseAdd.vue'
import CourseDetails from '../views/Courses/CourseDetails.vue'
import StudentDetails from '../views/Students/StudentDetails.vue'
import Login from '../views/Authentication/Login.vue'
import Error from '../views/Error.vue'
import AuthError from '../views/AuthError.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/get-students',
    name: 'StudentList',
    component: StudentList
  },
  {
    path:'/add-student',
    name:'StudentAdd',
    component:StudentEdit
  },
  {
    path:'/edit-student/:id',
    name:'StudentEdit',
    component:StudentEdit
  },
  {
    path:'/get-courses',
    name:'CourseList',
    component:CourseList
  },
  {
    path:'/add-course',
    name:'CourseAdd',
    component:CourseAdd
  },
  {
    path:'/details-course/:id',
    name:'CourseDetails',
    component:CourseDetails
  },
  {
    path:'/details-student/:id',
    name:'StudentDetails',
    component:StudentDetails
  },
  {
    path: '/error',
    name: 'ErrorPage',
    component: Error
  },
  {
    path: '/auth-error',
    name: 'AuthErrorPage',
    component: AuthError
  },
  {
    path: '/*',
    name: 'Error',
    component: Error
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
