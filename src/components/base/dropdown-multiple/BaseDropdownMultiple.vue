<template>
    <div class="dx-field-value">
        <DxDropDownBox
          ref="dropdownBox"
          :value="treeBoxValue"
          :show-clear-button="true"
          :data-source="treeDataSource"
          value-expr="ID"
          display-expr="name"
          @value-changed="syncTreeViewSelection($event)"
        >
          <template #content="{  }">
            <DxTreeView
              :ref="treeViewRefName"
              :data-source="treeDataSource"
              :select-by-click="true"
              :searchEnabled="true"
              placeholder="Tìm kiếm"
              noDataText="Không có dữ liệu"
              data-structure="plain"
              key-expr="ID"
              parent-id-expr="categoryId"
              selection-mode="multiple"
              show-check-boxes-mode="normal"
              display-expr="name"
              @content-ready="$event.component.selectItem(treeBoxValue)"
              @item-selection-changed="treeView_itemSelectionChanged($event)"
            />
          </template>
        </DxDropDownBox>
    </div>
</template>
<script>
import treeData from "../../../js/fake-data/data-dropdown-single"

import DxDropDownBox from 'devextreme-vue/drop-down-box';
import DxTreeView from 'devextreme-vue/tree-view';
export default {
    name: "BaseDropdownMultiple",
    components: {
        DxDropDownBox,
        DxTreeView,
    },

    data() {
        return {
            treeBoxValue: null,
            treeViewRefName: 'tree-view',
            treeDataSource: treeData,
        };
    },
    
    methods: {
        /**
         * @Description Xử lý sự kiện khi item được click và bind dữ liệu
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        syncTreeViewSelection() {
          if (!this.$refs[this.textBoxRefName]) return;
          if (!this.treeBoxValue) {
            this.$refs[this.textBoxRefName].instance.unselectAll();
          } else {
            this.$refs[this.textBoxRefName].instance.selectItem(this.treeBoxValue);
          }
        },

        /**
         * @Description Xử lý sự kiện cho item được trọn trong tree data
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        treeView_itemSelectionChanged(e) {
          this.treeBoxValue = e.component.getSelectedNodeKeys();
        },
    },
}
</script>
<style scoped>
    @import "../../../css/base/dropdown-multiple/base-dropdown-multiple.css";
</style>