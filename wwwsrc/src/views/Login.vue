<template>
    <div>
        <div class="login">
            <form v-if="loginForm" @submit.prevent="loginUser">
                <input type="email" v-model="creds.email" placeholder="email">
                <input type="password" v-model="creds.password" placeholder="password">
                <button type="submit">Login</button>
            </form>
            <form v-else @submit.prevent="register">
                <input type="text" v-model="newUser.username" placeholder="name">
                <input type="email" v-model="newUser.email" placeholder="email">
                <input type="password" v-model="newUser.password" placeholder="password">
                <button type="submit">Create Account</button>
            </form>
            <div @click="loginForm = !loginForm">
                <p v-if="loginForm">No account? Click to Register</p>
                <p v-else>Already have an account? Click to Login</p>
            </div>
        </div>
        <KeepList v-bind:keeps="publicKeeps" />
    </div>
</template>

<script>

    import KeepList from '@/components/KeepList.vue'

    export default {
        name: "login",
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                }
            };
        },
        beforeCreate() {
            if (this.$store.state.user.id) {
                this.$router.push({ name: "home" })
            }
        },
        computed: {
            publicKeeps() {
                return this.$store.state.publicKeeps;
            }
        },
        mounted() {
            this.$store.dispatch("getPublicKeeps");
        },
        components: {
            KeepList
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>