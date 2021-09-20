<template>
    <div class="content-wrap">
        <SalaryPolicyList/>
    </div>
</template>
<script>

// import { EventBus } from '../../main';
import SalaryPolicyList from "../../view/salary/SalaryPolicyList.vue"

export default {
    name: "TheContent",
    components: {
        SalaryPolicyList,
    },

}
</script>
<style scoped>

</style>